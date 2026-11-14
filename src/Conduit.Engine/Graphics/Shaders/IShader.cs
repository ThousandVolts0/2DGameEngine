namespace Conduit.Engine.Graphics.Shaders
{
    public interface IShader : IDisposable
    {
        public uint Handle { get; }

        void SetUniform<T>(string name, T value);
        void Bind();
    }
}
