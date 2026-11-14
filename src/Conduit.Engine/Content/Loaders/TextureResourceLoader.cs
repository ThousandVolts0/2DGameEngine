using Microsoft.Extensions.Logging;
using StbImageSharp;
namespace Conduit.Engine.Content.Loaders
{
    public class TextureResourceLoader : IResourceLoader<Texture>
    {
        private readonly ILogger<TextureResourceLoader> _logger;

        public TextureResourceLoader(ILogger<TextureResourceLoader> logger)
        {
            _logger = logger;
        }

        public Texture Load(string path, IResourceProvider provider)
        {
            using (var stream = File.OpenRead(path))
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
