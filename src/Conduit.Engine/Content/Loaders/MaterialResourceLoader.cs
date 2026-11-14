using Conduit.Engine.Graphics.Shaders;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Conduit.Engine.Content.Loaders
{
    public class MaterialResourceLoader : IResourceLoader<Material>
    {
        private readonly ILogger<MaterialResourceLoader> _logger;

        public MaterialResourceLoader(ILogger<MaterialResourceLoader> logger)
        {
            _logger = logger;   
        }

        public Material Load(string path, IResourceProvider provider)
        {
            if (!path.EndsWith(".mat")) 
                throw new ArgumentException("Invalid material file path. Must end with .mat");

            string json = File.ReadAllText(path);
            using JsonDocument doc = JsonDocument.Parse(json);

            JsonElement root = doc.RootElement;
            string shaderPath = root.GetProperty("shader").GetString()!;

            IShader shader = provider.Load<IShader>(shaderPath);

            // todo: add properties parse from file to shader
            //Dictionary<string, object> properties = JsonSerializer.Deserialize<Dictionary<string, object>>(root.GetProperty("properties"))!;
            //_logger.LogDebug("Loaded material from {Path} with shader {ShaderPath}", path, shaderPath);

            return new Material(shader);
        }
    }
}
