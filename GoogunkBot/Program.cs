using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using GoogunkBot.Modules;

namespace GoogunkBot
{
    internal class Program
    {
        private static DiscordClient DiscordClient;
        private static CommandsNextExtension CommandsNext;
        private static CopyPastaModule CopyPastaModule;
        private static AmaModule AmaModule;

        private static void Main(string[] args)
        {
            CopyPastaModule = new CopyPastaModule();
            AmaModule = new AmaModule();
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] arguments)
        {
            DiscordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = "NzIxNTAxNTUwNjIyODAyMDIz.XuWiog.gdHFWIfYXOLQOffeDT4z14e42Kg",
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
            
            await DiscordClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}