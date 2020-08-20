using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Config.Net;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using GoogunkBot.BackEnd.Configuration;
using GoogunkBot.Modules;
using GoogunkBot.Singletons;

namespace GoogunkBot
{
    internal class Program
    {
        private static DiscordClient _discordClient;
        private static CommandsNextExtension _commandsNext;
        private static CopyPastaModule _copyPastaModule;
        private static NamModule _namModule;
        private static ShittyVerseModule _shittyVerseModule;
        private static AmaModule _amaModule;
        private static IGoogunkBackendConfig _config;

        private static void Main(string[] args)
        {
            _config = new ConfigurationBuilder<IGoogunkBackendConfig>()
                .UseJsonConfig()
                .Build();
            _copyPastaModule = new CopyPastaModule();
            _amaModule = new AmaModule();
            _namModule = new NamModule();
            _shittyVerseModule = new ShittyVerseModule();
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] arguments)
        {
            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = _config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Error
            });
            
            _discordClient.MessageCreated += async e => { await CheckForCannedResponses(e); };

            _commandsNext = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new List<string>{"plz", "PLZ", "Plz"}
            });
            _commandsNext.RegisterCommands<ShittyModule>();
            _commandsNext.RegisterCommands<NamModule>();
            _commandsNext.RegisterCommands<ShittyVerseModule>();
            
            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task CheckForCannedResponses(MessageCreateEventArgs e)
        {
            var copyPasta = _copyPastaModule.CopyPastas.FirstOrDefault(x => x.Command == e.Message.Content.ToLower());
            if (copyPasta != null)
            {
                await e.Message.RespondAsync(copyPasta.Pasta);
                return;
            }

            switch (e.Message.Content.ToLower())
            {
                case "fuck you bot":
                    await e.Message.RespondAsync(_copyPastaModule.GetWaffle());
                    return;
                case "k ama":
                    await e.Message.RespondAsync("k");
                    break;
                case "keira":
                case "keira knightly":
                    await e.Message.RespondWithFileAsync(Path.Combine(Environment.CurrentDirectory,
                        $"Pictures\\Other\\keira.jpg"), TemporaryStrings.KeiraString);
                    break;
            }

            if (e.Message.Content.ToLower().EndsWith(" ama"))
            {
                await e.Message.RespondAsync(_amaModule.ConvertAmaString(e.Message.Content));
            }
        }
    }
}