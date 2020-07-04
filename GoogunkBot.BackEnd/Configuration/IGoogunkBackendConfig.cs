using System;
using System.Collections.Generic;
using System.Text;

namespace GoogunkBot.BackEnd.Configuration
{
    public interface IGoogunkBackendConfig
    {
        string ConnectionString { get; set; }
        string Token { get; set; }
    }
}
