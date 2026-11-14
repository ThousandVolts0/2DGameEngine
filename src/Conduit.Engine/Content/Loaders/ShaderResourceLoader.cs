using Conduit.Engine.Graphics;
using Conduit.Engine.Graphics.Shaders;
using Microsoft.Extensions.Logging;

namespace Conduit.Engine.Content.Loaders
{
    public class ShaderResourceLoader : IResourceLoader<IShader>
    {
        private readonly ILogger<ShaderResourceLoader> _logger;
        private readonly IGraphicsDevice _graphicsDevice;

        public ShaderResourceLoader(ILogger<ShaderResourceLoader> logger, IGraphicsDevice graphicsDevice)
        {
            _logger = logger;
            _graphicsDevice = graphicsDevice;
        }

        public IShader Load(string path, IResourceProvider provider)
        {
            if (!path.EndsWith(".shader"))
                throw new ArgumentException("Invalid shader file path. Must end with .shader");

            string file = File.ReadAllText(path);

            if (file.IndexOf("#type vertex") == -1 || file.IndexOf("#type fragment") == -1)
                throw new ArgumentException("Invalid shader file format. Must contain #type vertex and #type fragment sections.");

            int start = file.IndexOf("#type vertex") + "#type vertex".Length;
            int end = file.IndexOf("#type fragment");

            string vertexShaderSource = file.Substring(start, end - start).Trim();
            string fragmentShaderSource = file.Substring(end + "#type fragment".Length).Trim();

            ShaderSource source = new ShaderSource
            {
                Vertex = vertexShaderSource,
                Fragment = fragmentShaderSource
            };

            return _graphicsDevice.CreateShader(source);
        }
    }
}
