﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Config.Net;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using GoogunkBot.BackEnd.Configuration;
using GoogunkBot.Modules;
using GoogunkBot.Singletons;

namespace GoogunkBot
{
    internal class Program
    {
        private static DiscordClient DiscordClient;
        private static CommandsNextExtension CommandsNext;
        private static CopyPastaModule CopyPastaModule;
        private static NamModule NamModule;
        //private static ShittyVerseModule ShittyVerseModule;
        private static AmaModule AmaModule;
        private static IGoogunkBackendConfig Config;

        private static void Main(string[] args)
        {
            Config = new ConfigurationBuilder<IGoogunkBackendConfig>()
                .UseJsonConfig()
                .Build();
            CopyPastaModule = new CopyPastaModule();
            AmaModule = new AmaModule();
            NamModule = new NamModule();
            //ShittyVerseModule = new ShittyVerseModule();
            GameState.Initialize();
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] arguments)
        {
            DiscordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Error
            });
            
            DiscordClient.MessageCreated += async e =>
            {
                var (key, value) = CopyPastaModule.CopyPasta.FirstOrDefault(x => x.Key == e.Message.Content.ToLower());
                if (key != null)
                {
                    await e.Message.RespondAsync(value);
                    return;
                }

                if(e.Message.Content.ToLower() == "fuck you bot")
                {
                    await e.Message.RespondAsync(CopyPastaModule.GetWaffle());
                    return;
                }

                if (e.Message.Content.ToLower().EndsWith(" ama"))
                {
                    await e.Message.RespondAsync(AmaModule.ConvertAmaString(e.Message.Content));
                }
            };

            CommandsNext = DiscordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new List<string>{"plz", "PLZ", "Plz"}
            });
            CommandsNext.RegisterCommands<ShittyModule>();
            CommandsNext.RegisterCommands<NamModule>();
            //CommandsNext.RegisterCommands<ShittyVerseModule>();
            
            await DiscordClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}