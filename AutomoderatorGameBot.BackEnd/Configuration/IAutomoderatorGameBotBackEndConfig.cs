namespace AutomoderatorGameBot.BackEnd.Configuration
{
    public interface IAutomoderatorGameBotBackEndConfig
    {
        string ConnectionString { get; set; }
        string Token { get; set; }
        string CommandPrefix { get; set; } 
    }
}