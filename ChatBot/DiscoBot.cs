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
        DiscordClient _DiscordClient;

        public DiscoBot()
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
                    // Get text
                    string s = e.GetArg("words");

                    // Send request
                    Request r = new Request(s, myUser, AI);

                    // Save answer
                    Result res = AI.Chat(r);

                    // Output answer
                    await e.Channel.SendMessage(res.Output);
                });

            commands.CreateCommand("version")
             .Do(async (e) =>
             {
                    // Send Version
                    await e.Channel.SendMessage("Chatter v1.0");
            });

            _DiscordClient.ExecuteAndWait(async () =>
            {
                // Change Token
                await _DiscordClient.Connect("", TokenType.Bot);
            });
        }

        private void log(Object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
