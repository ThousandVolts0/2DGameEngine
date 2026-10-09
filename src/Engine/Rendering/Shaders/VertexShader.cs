using Silk.NET.OpenGL;

namespace Engine.Rendering.Shaders
{
    public class VertexShader : IShader
    {
        public uint Handle { get; }
        public VertexShader(GL gl, string source)
        {
            Handle = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(Handle, source);
            gl.CompileShader(Handle);
            gl.GetShader(Handle, ShaderParameterName.CompileStatus, out int status);

            if (status != (int)GLEnum.True)
                throw new Exception($"Vertex shader failed to compile: {gl.GetShaderInfoLog(Handle)}");
        }
    }
}
