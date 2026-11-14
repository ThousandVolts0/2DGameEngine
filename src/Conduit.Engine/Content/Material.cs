using Conduit.Engine.Graphics.Shaders;

namespace Conduit.Engine.Content
{
    public class Material
    {
        internal IShader Shader { get; }
        protected readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public Material(IShader shader)
        {
            Shader = shader;
        }

        public Material(IShader shader, Dictionary<string, object> properties) : this(shader)
        {
            _properties = properties;
        }

        public virtual Material Clone() => new Material(Shader, _properties);

        public void SetProperty(string name, object value)
        {
            _properties[name] = value;
        }

        public bool HasProperty<T>(string name) => TryGetProperty<T>(name, out _);

        public bool TryGetProperty<T>(string name, out T? value)
        {
            if (_properties.TryGetValue(name, out object? obj) && obj is T result)
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        public IReadOnlyDictionary<string, object> GetProperties() => _properties.AsReadOnly();

        public void ApplyProperties()
        {
            Shader.Bind();
            foreach(var property in _properties)
                Shader.SetUniform(property.Key, property.Value);
        }
    }
}
