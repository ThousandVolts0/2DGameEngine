namespace Conduit.Engine.Core.Game
{
    public class GameSettings
    {
        public GameWindowSettings Window { get; set; } = new();
        public GameLoggingSettings Logging { get; set; } = new();
    }
}
