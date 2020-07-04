using System.Collections.Generic;
using Bogus;

namespace GoogunkBot.Modules
{
    public class CopyPastaModule
    {
        private Faker Faker = new Faker();
        public Dictionary<string, string> CopyPasta = new Dictionary<string, string>
        {
            {
                "k",
                "You fucking do that every damn time I try to talk to you about anything even if it's not important you just say K and to be honest it makes me feel rejected and unheard like nothing would be better that that bullshit who the fuck just says k after you tell them something important I just don't understand how you think that's ok and I swear to god you're probably just gonna say k to this but when you do you'll know that you're slowly killing me inside"

            },
            {
                "penis",
                "🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶 🎶PENIS MUSIC🎶"
            },
            {
                "tiger",
                "Rising up, back on the dick\nDick my time, dick my chances\nWent the dickstance, now I'm back on my dick\nJust a dick and his will to survive\n\nSo many times, dick happens too fast\nYou trade your passion for dick\nDon't lose your grip on the dick of the past\nYou must fight just to keep dick alive\n\nIt's the dick of the tiger\nIt's the thrill of the fuck\nRising up to the dick of our rival\nAnd the last known survivor\nDicks his prey in the night\nAnd he's dicking us all with the dick of the tiger\n\nDick to dick, out in the heat\nDicking tough, staying horney\nThey stack the dicks still we dick to the street\nFor the dick with the skill to survive\n\nIt's the dick of the tiger\nIt's the thrill of the fuck\nRising up to the dick of our rival\nAnd the last known survivor\nDicks his prey in the night\nAnd he's dicking us all with the dick of the tiger\n\nRising up, straight to the top\nHad the nuts, got the glory\nWent the dickstance, now I'm not gonna stop\nJust a dick and his will to survive\n\nIt's the dick of the tiger\nIt's the thrill of the fuck\nRising up to the dick of our rival\nAnd the last known survivor\nDicks his prey in the night\nAnd he's dicking us all with the dick of the tiger\n\nThe dick of the tiger\nThe dick of the tiger\nThe dick of the tiger\nThe dick of the tiger"
            }
        };

        public string GetWaffle()
        {
            return Faker.WaffleText(Faker.Random.Int(1, 3));
        }
    }
}