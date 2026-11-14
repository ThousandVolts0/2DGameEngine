using Conduit.Engine.Content.Defaults;

namespace Conduit.Engine.Content
{
    public interface IResourceProvider : IDisposable
    {
        DefaultResources Defaults { get; }
        internal void InitializeDefaults();
        T Load<T>(string path) where T : class;
        bool Exists(string path);
    }
}
