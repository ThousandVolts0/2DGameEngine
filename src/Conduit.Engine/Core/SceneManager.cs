using Microsoft.Extensions.Logging;

namespace Conduit.Engine.Core
{
    public class SceneManager : IDisposable
    {
        private readonly Dictionary<string, Scene> _registeredScenes = new();
        public Scene? ActiveScene { get; private set; }
        public event Action<Scene>? SceneActivated;
        public event Action<Scene>? SceneDeactivated;

        private readonly ILogger<SceneManager> _logger;
        public SceneManager(ILogger<SceneManager> logger)
        {
            _logger = logger;
        }

        public void RegisterScene(Scene scene, string name)
        {
            if (_registeredScenes.ContainsKey(name))
                throw new InvalidOperationException($"Scene '{name}' is already registered.");

            _registeredScenes.Add(name, scene);
            _logger.LogDebug($"Scene '{name}' registered.");
        }

        public void UnregisterScene(string name)
        {
            if (!_registeredScenes.ContainsKey(name))
                throw new InvalidOperationException($"Scene '{name}' is not registered.");

            if (ActiveScene == _registeredScenes[name])
                throw new InvalidOperationException($"Cannot unregister the active '{name}' scene, set another scene active first");

            _registeredScenes.Remove(name);
            _logger.LogDebug($"Scene '{name}' unregistered.");
        }

        public void SetSceneActive(string name)
        {
            if (!_registeredScenes.TryGetValue(name, out Scene? scene))
                throw new InvalidOperationException($"Scene '{name}' is not registered.");

            if (ActiveScene != null)
                SceneDeactivated?.Invoke(ActiveScene);

            ActiveScene = scene;

            SceneActivated?.Invoke(ActiveScene);
        }

        public void Dispose()
        {
            foreach (var scene in _registeredScenes.Values)
            {
                if (scene is IDisposable disposableScene)
                    disposableScene.Dispose();
            }

            _registeredScenes.Clear();
            ActiveScene = null;
            SceneActivated = null; // Unsubscribe event handlers
            SceneDeactivated = null; // Unsubscribe event handlers
        }
    }
}
