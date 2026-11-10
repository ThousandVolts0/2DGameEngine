using Microsoft.Extensions.Logging;

namespace Engine.Core
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
            {
                _logger.LogWarning($"Scene '{name}' is already registered.");
                return;
            }

            _registeredScenes[name] = scene;
            _logger.LogDebug($"Scene '{name}' registered.");
        }

        public void UnregisterScene(string name)
        {
            if (!_registeredScenes.ContainsKey(name))
            {
                _logger.LogWarning($"Scene '{name}' is not registered.");
                return;
            }
            _registeredScenes.Remove(name);

            _logger.LogDebug($"Scene '{name}' unregistered.");
        }

        public void SetSceneActive(string name)
        {
            if (!_registeredScenes.TryGetValue(name, out Scene? scene))
            {
                _logger.LogWarning($"Scene '{name}' is not registered.");
                return;
            }

            if (ActiveScene != null)
                SceneDeactivated?.Invoke(ActiveScene);

            ActiveScene = scene;

            SceneActivated?.Invoke(ActiveScene);
        }

        public void Dispose()
        {
            foreach (var scene in _registeredScenes.Values)
                scene.Dispose();

            _registeredScenes.Clear();
            ActiveScene = null;
            SceneActivated = null; // Unsubscribe event handlers
            SceneDeactivated = null; // Unsubscribe event handlers
        }
    }
}
