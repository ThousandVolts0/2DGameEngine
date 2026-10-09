namespace Engine.Content.Loaders
{
    public interface IResourceLoader<T>
    {
        T Load(string path);
    }
}
