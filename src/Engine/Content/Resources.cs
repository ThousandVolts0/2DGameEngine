using Engine.Content.Loaders;

namespace Engine.Content
{
    public static class Resources
    {
        private static Dictionary<Type, object> _resourceLoaders = new Dictionary<Type, object>()
        {
            { typeof(Texture), new ImageResourceLoader() }
        };

        public static string GetResourcePath(string resourceName)
        {
            resourceName = resourceName.Replace('\\', '/');

            if (resourceName.StartsWith("Resources/"))
                resourceName = resourceName.Substring("Resources/".Length);

            return Path.Combine(AppContext.BaseDirectory, "Resources", resourceName);
        }

        public static T Load<T>(string path) where T : class
        {
            var loader = _resourceLoaders[typeof(T)] as IResourceLoader<T>;
            if (loader == null)
                throw new NotSupportedException($"No resource loader found for type {typeof(T).Name}");

            string resourcePath = GetResourcePath(path);
            

            if (!File.Exists(resourcePath))
                throw new FileNotFoundException($"Resource at path '{path}' not found.");

            return loader.Load(resourcePath);
        }
    }
}