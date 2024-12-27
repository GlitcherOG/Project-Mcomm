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
using SSX3_Server.EAServer;
using DSharpPlus.Entities;

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
        public async Task Main(string Token)
        {
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = Token,
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
        public async Task OnlineCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("There are " + EAServerManager.Instance.clients.Count + " Clients Online.");
        }

        [Command("users")]
        public async Task UsersCommand(CommandContext ctx)
        {
            List<string> strings = new List<string>();

            for (int i = 0; i < EAServerManager.Instance.clients.Count; i++)
            {
                if (EAServerManager.Instance.clients[i].userData.Name!="")
                {
                    strings.Add(EAServerManager.Instance.clients[i].userData.Name);
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            embed.Title = "Online Player's";

            for (int i = 0; i < strings.Count; i++)
            {
                embed.Description += strings[i] + "\n";
            }

            embed.WithThumbnail("https://media.discordapp.net/attachments/1218902383568883794/1219202018816692255/Bot.png");

            await ctx.RespondAsync(embed);
        }

        [Command("persona")]
        public async Task PersonaCommand(CommandContext ctx)
        {
            List<string> strings = new List<string>();

            for (int i = 0; i < EAServerManager.Instance.clients.Count; i++)
            {
                if (EAServerManager.Instance.clients[i].LoadedPersona.Name != "")
                {
                    strings.Add(EAServerManager.Instance.clients[i].LoadedPersona.Name);
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            embed.Title = "Online Persona's";

            for (int i = 0; i < strings.Count; i++)
            {
                embed.Description += strings[i] + "\n";
            }

            embed.WithThumbnail("https://media.discordapp.net/attachments/1218902383568883794/1219202018816692255/Bot.png");

            await ctx.RespondAsync(embed);
        }

        [Command("highscore")]
        public async Task highscoreCommand(CommandContext ctx, string args = "")
        {
            int DatabaseEntry = 0;

            var Database = EAServerManager.Instance.highscoreDatabase.courseEntries[DatabaseEntry];

            if (args != "")
            {
                DatabaseEntry = HighscoreDatabase.IDToName.FirstOrDefault(x => x.Value == args).Key;
            }

            List<string> strings = new List<string>();

            for (int i = 0; i < Database.Entries.Count; i++)
            {
                strings.Add(Database.Entries[i].Rank + " - " + Database.Entries[i].Name + " - " + Database.Entries[i].Score);
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            embed.Title = HighscoreDatabase.IDToName[DatabaseEntry] + " Highscores";

            for (int i = 0; i < strings.Count; i++)
            {
                embed.Description += strings[i] + "\n";
            }

            embed.WithThumbnail("https://media.discordapp.net/attachments/1218902383568883794/1219202018816692255/Bot.png");

            await ctx.RespondAsync(embed);
        }

        [Command("kickuser")]
        public async Task kickuserCommand(CommandContext ctx, string args)
        {
            int ClientID = -1;
            for (int i = 0; i < EAServerManager.Instance.clients.Count; i++)
            {
                if (EAServerManager.Instance.clients[i].userData.Name == ctx.RawArgumentString)
                {
                    ClientID = EAServerManager.Instance.clients[i].ID;
                    EAServerManager.Instance.DestroyClient(ClientID, true);
                    await ctx.RespondAsync("Kicking User" + ctx.RawArgumentString);
                }
            }

            if(ClientID==-1)
            {
                await ctx.RespondAsync("Unable to Find User");
            }
        }

        [Command("kickpersona")]
        public async Task kickpersonaCommand(CommandContext ctx)
        {
            int ClientID = -1;
            for (int i = 0; i < EAServerManager.Instance.clients.Count; i++)
            {
                if (EAServerManager.Instance.clients[i].LoadedPersona.Name == ctx.RawArgumentString)
                {
                    ClientID = EAServerManager.Instance.clients[i].ID;
                    EAServerManager.Instance.DestroyClient(ClientID, true);
                    await ctx.RespondAsync("Kicking Persona" + ctx.RawArgumentString);
                }
            }

            if (ClientID == -1)
            {
                await ctx.RespondAsync("Unable to Find User");
            }
        }
    }
}
