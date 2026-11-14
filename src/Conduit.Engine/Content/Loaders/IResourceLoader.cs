namespace Conduit.Engine.Content.Loaders
{
    public interface IResourceLoader<T>
    {
        T Load(string path, IResourceProvider provider);
    }
}
