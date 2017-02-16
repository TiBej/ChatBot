using System;
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace ChatBot
{
    class DiscoBot
    {
        private DiscordClient _DiscordClient;
        private bool _IsActive = true;


        public DiscoBot(string TOKEN)
        {
            // Setup AIML
            Bot AI = new Bot();
            AI.loadSettings();
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = false;

            // Setup User
            AIMLbot.User myUser = new AIMLbot.User("Username", AI);

            AI.isAcceptingUserInput = true;

            // Discord Settings
            _DiscordClient = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = log;
            });

            _DiscordClient.UsingCommands(x =>
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
            });

            var commands = _DiscordClient.GetService<CommandService>();

            // A list of all commands
            commands.CreateCommand("help")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Get help with +help <text>");
                    await e.Channel.SendMessage("Send a message with ++ <text>");
                    await e.Channel.SendMessage("Check Version with +version <text>");
                });

            commands.CreateCommand("+")
                .Parameter("words", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    // do if enabled          
                    if(_IsActive == true)
                    {
                        // Get text
                        string s = e.GetArg("words");

                        // Send request
                        Request r = new Request(s, myUser, AI);

                        // Save answer
                        Result res = AI.Chat(r);

                        // Output answer
                        await e.Channel.SendMessage(res.Output);
                    }
                });

            commands.CreateCommand("version")
                .Do(async (e) =>
                {
                    // Send version
                    await e.Channel.SendMessage("Chatter v1.0");
                });

            commands.CreateCommand("IsActive")
                .Parameter("input", ParameterType.Unparsed)
                .AddCheck((cm, u, ch) => u.ServerPermissions.Administrator)
                .Do(async (e) =>
                {
                    // Input
                    try
                    {
                        // get set value
                        _IsActive = Convert.ToBoolean(e.GetArg("input"));

                        // confirm user
                        await e.Channel.SendMessage((_IsActive == true) ? "activated" : "deactivated");
                    }
                    catch
                    {
                        await e.Channel.SendMessage("Invalid input!");
                    }
                });

            // Build connection
            _DiscordClient.ExecuteAndWait(async () =>
            {
                    await _DiscordClient.Connect(TOKEN, TokenType.Bot);
            });
        }

        private void log(Object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
