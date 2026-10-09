using StbImageSharp;
namespace Engine.Content.Loaders
{
    public class ImageResourceLoader : IResourceLoader<Texture>
    {
        public Texture Load(string path)
        {
            string resourcePath = Resources.GetResourcePath(path);
            using (var stream = File.OpenRead(resourcePath))
            {
                ImageResult imageResult = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                return new Texture
                {
                    ImagePixels = imageResult.Data,
                    Width = imageResult.Width,
                    Height = imageResult.Height
                };
            }
        }
    }
}
