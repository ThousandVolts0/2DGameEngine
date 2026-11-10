using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Game
{
    public class GameLoggingSettings
    {
        public LogLevel MinimumLevel { get; set; } = LogLevel.Information;
        public bool UseConsole { get; set; } = false;
    }
}
