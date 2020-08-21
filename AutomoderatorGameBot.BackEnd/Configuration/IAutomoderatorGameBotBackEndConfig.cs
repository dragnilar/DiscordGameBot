using System;
using System.Collections.Generic;
using System.Text;

namespace AutomoderatorGameBot.BackEnd.Configuration
{
    public interface IAutomoderatorGameBotBackEndConfig
    {
        string ConnectionString { get; set; }
        string Token { get; set; }
    }
}
