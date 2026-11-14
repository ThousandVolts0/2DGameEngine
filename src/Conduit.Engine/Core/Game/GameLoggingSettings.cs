using Microsoft.Extensions.Logging;

namespace Conduit.Engine.Core.Game
{
    public class GameLoggingSettings
    {
        public LogLevel MinimumLevel { get; set; } = LogLevel.Information;
        public bool UseConsole { get; set; } = false;
    }
}
