using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using Engine.Graphics;
using Engine.Rendering;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using Engine.Core.Game.Abstractions;

namespace Engine.Core.Game
{
    public class Game : IDisposable
    {
        public GameSettings Settings { get; }
        
        private readonly IWindow _window;
        private readonly RenderPipeline _renderPipeline;
        private readonly SceneManager _sceneManager;
        private readonly IGraphicsDevice _graphicsDevice;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<Game> _logger;
        private IInputContext? _input = null;
        private GameContext? _context = null;

        internal Game(
            GameSettings settings,
            IWindow window,
            SceneManager sceneManager,
            RenderPipeline renderPipeline,
            IGraphicsDevice graphicsDevice,
            ILoggerFactory loggerFactory)
        {
            Settings = settings;

            _sceneManager = sceneManager;
            _window = window;
            _renderPipeline = renderPipeline;
            _graphicsDevice = graphicsDevice;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<Game>();
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
            _context = new GameContext(
                _logger,
                _sceneManager,
                new GameInput(_input, _loggerFactory.CreateLogger<GameInput>()), 
                new GameWindow(_window));

            _graphicsDevice.Initialize(_window);
            _graphicsDevice.SetClearColor(Color.CornflowerBlue);
        }

        private void OnUpdate(double dt)
        {
        }

        private void OnRender(double dt)
        {
            _graphicsDevice.Clear();
            if (_sceneManager.ActiveScene != null)
                _renderPipeline.Render(_sceneManager.ActiveScene);
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
            _input?.Dispose();
            
            _window.Dispose();
        }

        public static GameBuilder Create() => new GameBuilder();
    }

}
