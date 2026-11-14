using Silk.NET.Maths;
using Silk.NET.Windowing;
using Microsoft.Extensions.Logging;
using Conduit.Engine.Graphics.Shaders;
using Conduit.Engine.Graphics;
using Conduit.Engine.Content.Loaders;
using Conduit.Engine.Content;
using Conduit.Engine.Rendering;

namespace Conduit.Engine.Core.Game
{
    public class GameBuilder
    {
        private readonly GameSettings _settings = new();
        private readonly Dictionary<string, Scene> _scenes = new();
        private string? _activeSceneId = null;

        public GameBuilder AddScene<T>(string name) where T : Scene, new()
        {
            if (_scenes.ContainsKey(name))
                throw new ArgumentException($"Scene '{name}' is already registered.");

            _scenes[name] = new T();
            return this;
        }

        public GameBuilder SetActiveScene(string name)
        {
            if (!_scenes.TryGetValue(name, out Scene? scene))
                throw new ArgumentException($"Scene '{name}' is not registered.");

            _activeSceneId = name;
            return this;
        }

        public GameBuilder Configure(Action<GameSettings> configure)
        {
            configure(_settings);
            return this;
        }

        public Game Build()
        {
            if (_activeSceneId == null)
                throw new ArgumentNullException("The active scene has not been set");

            WindowOptions windowOptions = WindowOptions.Default with
            {
                Size = new Vector2D<int>(_settings.Window.Width, _settings.Window.Height),
                Title = _settings.Window.Title,
                VSync = _settings.Window.UseVSync,
            };

            IWindow window = Window.Create(windowOptions);

            Action<ILoggingBuilder> config = (logger) =>
            {
                logger.SetMinimumLevel(_settings.Logging.MinimumLevel);

                if (_settings.Logging.UseConsole)
                    logger.AddConsole();
            };

            ILoggerFactory loggerFactory = LoggerFactory.Create(config);

            ILogger<OpenGLGraphicsDevice> graphicsLogger = loggerFactory.CreateLogger<OpenGLGraphicsDevice>();
            ILogger<RenderPipeline> renderPipelineLogger = loggerFactory.CreateLogger<RenderPipeline>();
            ILogger<SceneManager> sceneLogger = loggerFactory.CreateLogger<SceneManager>();

            IGraphicsDevice graphicsDevice = new OpenGLGraphicsDevice(graphicsLogger);

            Dictionary<Type, object> _resourceLoaders = new()
            {
                { typeof(Texture), new TextureResourceLoader(loggerFactory.CreateLogger<TextureResourceLoader>()) },
                { typeof(Material), new MaterialResourceLoader(loggerFactory.CreateLogger<MaterialResourceLoader>()) },
                { typeof(IShader), new ShaderResourceLoader(loggerFactory.CreateLogger<ShaderResourceLoader>(), graphicsDevice) }
            };

            IResourceProvider resources = new Resources(_resourceLoaders);
            RenderPipeline renderPipeline = new(graphicsDevice, window, renderPipelineLogger, resources);
            SceneManager sceneManager = new(sceneLogger);

            foreach (var kvp in _scenes)
                sceneManager.RegisterScene(kvp.Value, kvp.Key);

            Game game = new(_settings, window, sceneManager, resources, renderPipeline, graphicsDevice, loggerFactory, _activeSceneId);
            return game;
        }
    }
}
