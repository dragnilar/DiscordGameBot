namespace AutomoderatorGameBot.BackEnd.Models
{
    public class MiniGameChoice
    {
        public int Id { get; set; }
        public string ChoiceName { get; set; }
        public string SpecialResultText { get; set; }
        public string RegularResultText { get; set; }
        public string FailResultText { get; set; }
        public string MiniGameName { get; set; }
        public long SpecialResultMoney { get; set; }
        public long RegularResultMoney { get; set; }
        public int SpecialResultChance { get; set; }
        public int RegularResultChance { get; set; }
        public int FailResultChance { get; set; }
        public int? SpecialRewardId { get; set; }
        public virtual Item SpecialReward { get; set; }
        
        
    }
}