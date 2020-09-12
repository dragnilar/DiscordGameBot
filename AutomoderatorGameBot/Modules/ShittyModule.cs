using System;
using System.IO;
using System.Threading.Tasks;
using Bogus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace AutomoderatorGameBot.Modules
{
    public class ShittyModule : BaseCommandModule
    {
        [Command("box")]
        [Description("Gives you a random picture of a lovely cardboard box.")]
        public async Task Box(CommandContext ctx)
        {
            var faker = new Faker();
            var roll = faker.Random.Int(1, 45);
            var response = Path.Combine(Environment.CurrentDirectory, $"Pictures\\Boxes\\box{roll}.jpg");
            await ctx.RespondWithFileAsync(response);
        }

        [Command("urine")]
        [Description("Congratulate someone on bathing in Urine.")]
        public async Task Urine(CommandContext ctx)
        {
            var name = ctx.RawArgumentString;
            await ctx.RespondAsync(
                $"Congratulations {name}! You've essentially bathed yourself in urine and I hope you have a lot to show for it. Like diarrhea, muscle soreness, fatigue, and a fever. You also dont know what they could've ingested and how harmful the urine was. The only POSSIBLY example of consuming pee is when you're dying of dehydration in the middle of the fucking Sahara.");
        }

        [Command("bombastic")]
        [Description("All about Mr.Bombastic")]
        public async Task Bombastic(CommandContext ctx)
        {
            await ctx.RespondAsync(
                "Biggie Cheese, the rapping rat that you all know today, gained popularity in 2006 after the release of the movie Barnyard. Before the movie, he was already known as “Mr. Steal Yo Girl”, or the “guy she told you not to worry about”. But after the movie featured him rapping his debut hit “Mr. Boombastic,” he turned ultra-famous in a matter of seconds. Because he has made so much money and gained so much fame and popularity from this hit single, he has not yet released another album. A rumoured mixtape, “Back at the Barnyard” is expected to arrive soon.\r\n\r\nHe stated – in the 2006 interview “Behind the Cheese” with MTV – that his music is heavily influenced by his life. He stated that his tour in Afganistan has inspired many of his hard-hitting and deep rhythmic patterns and that some of the more complex chord changes (such as the C sharp major to the C double flat minor flat five) are meant to represent not only the duality of man but also the gruelling reality of war.\r\n\r\nHe also takes heavy influence from his first public relationship with Sherie Williams. It is known that this border-line abusive relationship ended with a bang and left a great mark on Mr. Cheese. In his defence statement, he told juries that: “I'mma neva' be da' same again ya'. I luv’d'a an' she straight up lied to ma' face!”\r\n\r\nHe has been nominated for 13 Grammies, and a total of 27 other awards including the Grand Prix du Disque, the Silver Clef, the Youtube Music Award, the MTV Africa Music Awards. However, he has not won a single award, allegedly due to his controversial lyrics which have dealt with topics such as oppression, LGBT rights, freedom of speech, and totalitarianism around the world. Despite this, he continues to persevere.");
        }

        [Command("shittyama")]
        [Description("Create a random shitty ama topic.")]
        public async Task ShittyAma(CommandContext ctx)
        {
            var faker = new Faker();
            var roll = faker.Random.Int(1, 3);


            switch (roll)
            {
                case 1:
                    await ctx.RespondAsync(
                        $"I am {faker.Person.FullName},  the inventor of the  {faker.Commerce.ProductName()}, which is {faker.Commerce.ProductDescription()}. AMA! ");
                    break;
                case 2:
                    await ctx.RespondAsync(
                        $"I am {faker.Rant.Review("penis")}, I ate a human {faker.Hacker.Noun()}. AMA! ");
                    break;
                default:
                    await ctx.RespondAsync(
                        $"{faker.Hacker.Phrase()}, you won't fuck-{faker.Hacker.IngVerb()} touch my {faker.System.FileName()}. AMA! ");
                    break;
            }
        }

        [Command("randombs")]
        [Description("Gives you a bunch of random bullshit text.")]
        public async Task RandomBs(CommandContext ctx)
        {
            var faker = new Faker();
            var waffle = faker.WaffleText(faker.Random.Int(1, 3));
            await ctx.RespondAsync(waffle);
        }
    }
}