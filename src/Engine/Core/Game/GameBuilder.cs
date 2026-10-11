using Engine.Rendering;
using Runtime;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Engine.Graphics;

namespace Engine.Core.Game
{
    public class GameBuilder
    {
        private GameSettings _settings = new GameSettings();

        public GameBuilder Window(int width, int height, string title = "New Game")
        {
            _settings.Window.Width = width;
            _settings.Window.Height = height;
            _settings.Window.Title = title;
            return this;
        }

        public Game Build()
        {
            WindowOptions windowOptions = WindowOptions.Default with
            {
                Size = new Vector2D<int>(_settings.Window.Width, _settings.Window.Height),
                Title = _settings.Window.Title,
                VSync = _settings.Window.UseVSync,
            };

            IWindow window = Silk.NET.Windowing.Window.Create(windowOptions);
            IGraphicsDevice graphicsDevice = new OpenGLGraphicsDevice();
            CanvasRegistry canvasRegistry = new CanvasRegistry();
            RenderPipeline renderPipeline = new RenderPipeline(graphicsDevice, window);

            GameServices services = new GameServices
            {
                Window = window,
                Canvases = canvasRegistry,
                Graphics = graphicsDevice,
                RenderPipeline = renderPipeline
            };

            Game game = new Game(_settings, services);
            return game;
        }
    }
}
