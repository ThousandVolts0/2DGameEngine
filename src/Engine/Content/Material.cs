using Engine.Graphics.Shaders;

namespace Engine.Data
{
    public class Material
    {
        public IShader Shader { get; }

        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public Material(IShader shader)
        {
            Shader = shader;
        }

        public Material(IShader shader, Dictionary<string, object> properties) : this(shader)
        {
            _properties = properties;
        }

        public void SetProperty(string name, object value)
        {
            _properties[name] = value;
        }

        public void ApplyProperties()
        {
            Shader.Bind();
            foreach(var property in _properties)
                Shader.SetUniform(property.Key, property.Value);
        }
    }
}
