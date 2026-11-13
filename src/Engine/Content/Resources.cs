using Engine.Content.Defaults;
using Engine.Content.Loaders;
using Engine.Data;

namespace Engine.Content
{
    public class Resources : IResourceProvider
    {
        private readonly Dictionary<Type, object> _resourceLoaders;

        private DefaultResources? _defaults = null;
        public DefaultResources Defaults => _defaults
            ?? throw new InvalidOperationException("Default resources have not been initialized yet");

        public Resources(Dictionary<Type, object> resourceLoaders)
        {
            _resourceLoaders = resourceLoaders;
        }

        public void InitializeDefaults()
        {
            if (_defaults != null)
                throw new InvalidOperationException(
                    "Default resources have already been initialized.");

            Material unlitLoaded = Load<Material>("Defaults/Materials/unlit.mat");
            UnlitMaterial unlitMat = new(unlitLoaded.Shader, new(unlitLoaded.GetProperties()));

            DefaultMaterials defaultMaterials = new(unlitMat);
            _defaults = new(defaultMaterials);
        }

        public static string GetResourcePath(string resourceName)
        {
            resourceName = resourceName.Replace('\\', '/'); 

            if (resourceName.StartsWith("Resources/", StringComparison.OrdinalIgnoreCase))
                resourceName = resourceName.Substring("Resources/".Length);

            return Path.Combine(AppContext.BaseDirectory, "Resources", resourceName);
        }

        public T Load<T>(string path) where T : class
        {
            var loader = (IResourceLoader<T>)_resourceLoaders[typeof(T)];
            if (loader == null)
                throw new NotSupportedException($"No resource loader found for type {typeof(T).Name}");

            string resourcePath = GetResourcePath(path);

            if (!File.Exists(resourcePath))
                throw new FileNotFoundException($"Resource at path '{path}' not found.");

            return loader.Load(resourcePath, this);
        }

        public bool Exists(string path) => File.Exists(GetResourcePath(path));

        public void Dispose()
        {
            foreach (var loader in _resourceLoaders.Values)
            {
                if (loader is IDisposable disposableLoader)
                    disposableLoader.Dispose();
            }

            _resourceLoaders.Clear();
            _defaults = null;
        }
    }
}