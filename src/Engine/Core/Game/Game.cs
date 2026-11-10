using Engine.Content;
using Engine.Core.Game.Abstractions;
using Engine.Graphics;
using Engine.Rendering;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Drawing;
using System.Resources;

namespace Engine.Core.Game
{
    public sealed class Game : IDisposable
    {
        public GameSettings Settings { get; }
        
        private readonly IWindow _window;
        private readonly RenderPipeline _renderPipeline;
        private readonly IGraphicsDevice _graphicsDevice;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IResourceProvider _resources;
        private readonly SceneManager _scenes;
        private readonly ILogger<Game> _logger;
        private readonly string _startSceneId;

        private IInputContext? _input = null;
        private GameContext? _context = null;

        internal Game(
            GameSettings settings,
            IWindow window,
            SceneManager scenes,
            IResourceProvider resources,
            RenderPipeline renderPipeline,
            IGraphicsDevice graphicsDevice,
            ILoggerFactory loggerFactory,
            string startSceneId)
        {
            Settings = settings;

            _scenes = scenes;
            _resources = resources;
            _window = window;
            _renderPipeline = renderPipeline;
            _graphicsDevice = graphicsDevice;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<Game>();
            _startSceneId = startSceneId;
        }

        public void Run()
        {
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;
            _window.Closing += OnClose;
            _window.Resize += OnResize;

            _window.Run();
            _window.Dispose();
        }

        private void OnLoad()
        {
            _logger.LogDebug("Window loaded");

            _input = _window.CreateInput();

            GameInput inputApi = new GameInput(_input, _loggerFactory.CreateLogger<GameInput>());
            GameWindow windowApi = new GameWindow(_window, _loggerFactory.CreateLogger<GameWindow>());

            _context = new GameContext(
                _logger,
                _scenes,
                _resources,
                inputApi,
                windowApi);

            _scenes.SceneActivated += LoadScene;
            _scenes.SceneDeactivated += scene => scene.Deactivate(_context);
            _scenes.SetSceneActive(_startSceneId);

            _graphicsDevice.Initialize(_window);
            _graphicsDevice.SetClearColor(Color.CornflowerBlue);
            _resources.InitializeDefaults();
        }

        private void LoadScene(Scene scene)
        {
            if (_context == null)
                throw new InvalidOperationException("Trying to load scene without initialized game context");

            scene.Initialize(_context); // Silently fails if already initialized
            scene.Activate(_context);
        }

        private void OnUpdate(double dt)
        {
        }

        private void OnRender(double dt)
        {
            _graphicsDevice.Clear();
            if (_scenes.ActiveScene != null)
                _renderPipeline.Render(_scenes.ActiveScene);
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

            _context?.Input.Dispose();
            _context?.Window.Dispose();

            _scenes.Dispose();
            _input?.Dispose();
        }
    }

}
