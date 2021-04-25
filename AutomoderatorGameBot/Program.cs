using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.Configuration;
using AutomoderatorGameBot.Modules;
using Config.Net;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Emzi0767.Utilities;
using Microsoft.Extensions.Logging;

namespace AutomoderatorGameBot
{
    internal class Program
    {
        private static DiscordClient _discordClient;
        private static CommandsNextExtension _commandsNext;
        private static CopyPastaModule _copyPastaModule;
        private static ShittyVerseModule _shittyVerseModule;
        private static AmaModule _amaModule;
        private static IAutomoderatorGameBotBackEndConfig _config;
        public static InteractivityExtension InteractivityExtension;
        private static ReactionModule _reactionModule;

        private static void Main(string[] args)
        {
            _config = new ConfigurationBuilder<IAutomoderatorGameBotBackEndConfig>()
                .UseJsonConfig()
                .Build();
            _copyPastaModule = new CopyPastaModule();
            _reactionModule = new ReactionModule();
            _amaModule = new AmaModule();
            _shittyVerseModule = new ShittyVerseModule();
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] arguments)
        {
            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = _config.Token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Information
            });

            _discordClient.MessageCreated += CheckForCannedResponses;

            _commandsNext = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new List<string> {"plz", "PLZ", "Plz"}
            });
            _commandsNext.RegisterCommands<ShittyModule>();
            _commandsNext.RegisterCommands<ShittyVerseModule>();
            _commandsNext.RegisterCommands<CopyPastaModule>();

            InteractivityExtension = _discordClient.UseInteractivity(new InteractivityConfiguration());
            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task DiscordClientOnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private static async Task CheckForCannedResponses(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsCurrent) return;

            await _reactionModule.ProcessReactions(e, _discordClient);

            var copyPastaRun = await _copyPastaModule.ProcessCopyPastas(e);
            if (copyPastaRun) return;

            var videoPasta = _copyPastaModule.VideoPastas.FirstOrDefault(x => x.Keyword == e.Message.Content.ToLower());
            if (videoPasta != null)
            {
                await using var videoFileStream = new FileStream(videoPasta.FilePath, FileMode.Open, FileAccess.Read);
                await new DiscordMessageBuilder().WithContent(videoPasta.Description).WithFile(videoFileStream)
                    .SendAsync(e.Channel);
            }

            if (e.Message.Content.ToLower().EndsWith(" ama"))
                await _amaModule.ProcessReactions(e);
        }
    }
}