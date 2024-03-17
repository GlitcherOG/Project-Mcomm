using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SSX3_Server.EAServer;

namespace SSX3_Server.DiscordBot
{
    public class MainBot
    {
        //TODO
        //MORE COMMANDS
        //HIGHSCORE
        //LOBBY
        //SERVER MESSAGE
        //DISCORD LINK
        //MAKE ACCOUNT

        public DiscordClient Client { get; set; }
        public CommandsNextExtension Commands { get; set; }
        public async Task Main()
        {
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = File.ReadAllText("Token.txt"),
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Error
            };
            Client = new DiscordClient(discordConfig);
            var commands = Client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<MainCommands>();

            Client.Ready += Client_Ready;
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }

    public class MainCommands : BaseCommandModule
    {
        [Command("online")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("There are " + EAServerManager.Instance.clients.Count + " Clients Online.");
        }
    }
}
