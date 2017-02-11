using System;
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    class DiscoBot
    {
        DiscordClient _DiscordClient;

        public DiscoBot()
        {
            _DiscordClient = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _DiscordClient.UsingCommands(x =>
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
            });

            var commands = _DiscordClient.GetService<CommandService>();

            commands.CreateCommand("+")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi");
                });

            _DiscordClient.ExecuteAndWait(async () =>
            {
                await _DiscordClient.Connect("", TokenType.Bot);
            });
        }

        private void Log(Object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
