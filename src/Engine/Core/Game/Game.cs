using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using Engine.Graphics;
using Engine.Rendering;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;

namespace Engine.Core.Game
{
    public class Game : IDisposable
    {
        public GameSettings Settings { get; }
        public IWindow Window { get; }
        public SceneManager SceneManager { get; }
        public IInputContext Input { get; private set; } = null!;

        private readonly RenderPipeline _renderPipeline;
        private readonly IGraphicsDevice _graphicsDevice;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<Game> _logger;

        public double FPS => Window.FramesPerSecond;

        internal Game(
            GameSettings settings,
            IWindow window,
            SceneManager sceneManager,
            RenderPipeline renderPipeline,
            IGraphicsDevice graphicsDevice,
            ILoggerFactory loggerFactory)
        {
            Settings = settings;
            SceneManager = sceneManager;
            Window = window;

            _renderPipeline = renderPipeline;
            _graphicsDevice = graphicsDevice;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<Game>();
        }

        public void Run()
        {
            Window.Load += OnLoad;
            Window.Update += OnUpdate;
            Window.Render += OnRender;
            Window.Closing += OnClose;
            Window.Resize += OnResize;

            Window.Run();
            Window.Dispose();
        }

        private void OnLoad()
        {
            _logger.LogDebug("Window loaded");
            OnSceneLoad(SceneManager.ActiveScene!);

            SceneManager.SceneActivated += OnSceneLoad;

            Input = Window.CreateInput();
            _graphicsDevice.Initialize(Window.CreateOpenGL());
            _graphicsDevice.SetClearColor(Color.CornflowerBlue);
        }

        private void OnUpdate(double dt)
        {
        }

        private void OnSceneLoad(Scene scene)
        {

        }

        private void OnRender(double dt)
        {
            _graphicsDevice.Clear();
            if (SceneManager.ActiveScene != null)
                _renderPipeline.Render(SceneManager.ActiveScene);
        }

        private void OnClose()
        {

        }

        private void OnResize(Vector2D<int> newSize)
        {

        }

        public void Dispose()
        {
            _loggerFactory.Dispose();
            Window.Dispose();
        }

        public static GameBuilder Create() => new GameBuilder();
    }

}
