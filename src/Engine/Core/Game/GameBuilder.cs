using Engine.Rendering;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Engine.Graphics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Silk.NET.Input;

namespace Engine.Core.Game
{
    public class GameBuilder
    {
        private readonly GameSettings _settings = new();
        private Action<ILoggingBuilder>? _configureLogging = null;
        private Scene? _activeScene = null;

        public GameBuilder WindowOptions(Action<GameWindowSettings> configure)
        {
            configure?.Invoke(_settings.Window);
            return this;
        }

        public GameBuilder StartingScene(Scene scene)
        {
            _activeScene = scene;
            return this;
        }

        public GameBuilder Logging(Action<ILoggingBuilder> configure)
        {
            _configureLogging = configure;
            return this;
        }

        public Game Build()
        {
            if (_activeScene == null)
                throw new ArgumentNullException("The active scene has not been set");

            WindowOptions windowOptions = Silk.NET.Windowing.WindowOptions.Default with
            {
                Size = new Vector2D<int>(_settings.Window.Width, _settings.Window.Height),
                Title = _settings.Window.Title,
                VSync = _settings.Window.UseVSync,
            };

            IWindow window = Window.Create(windowOptions);

            IGraphicsDevice graphicsDevice = new OpenGLGraphicsDevice();
            RenderPipeline renderPipeline = new(graphicsDevice, window);
            SceneManager sceneManager = new(_activeScene);

            ILoggerFactory loggerFactory = NullLoggerFactory.Instance;
            if (_configureLogging != null)
                loggerFactory = LoggerFactory.Create(_configureLogging);

            Game game = new Game(_settings, window, sceneManager, renderPipeline, graphicsDevice, loggerFactory);
            return game;
        }
    }
}
