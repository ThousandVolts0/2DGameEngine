using Conduit.Engine.Content;
using Microsoft.Extensions.Logging;

namespace Conduit.Engine.Core.Game.Abstractions
{
    public class GameContext
    {
        public ILogger<Game> Logger { get; }
        public SceneManager Scenes { get; }
        public IResourceProvider Resources { get; }
        public GameInput Input { get; }
        public GameWindow Window { get; }

        public GameContext(
            ILogger<Game> logger, 
            SceneManager scenes, 
            IResourceProvider resources,
            GameInput input,
            GameWindow window)
        {
            Logger = logger;
            Resources = resources;
            Scenes = scenes;
            Input = input;
            Window = window;
        }
    }
}
