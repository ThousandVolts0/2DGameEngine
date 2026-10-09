using Silk.NET.OpenGL;

namespace Engine.Rendering.Shaders
{
    public class FragmentShader : IShader
    {
        public uint Handle { get; }
        public FragmentShader(GL gl, string source)
        {
            Handle = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(Handle, source);
            gl.CompileShader(Handle);
            gl.GetShader(Handle, ShaderParameterName.CompileStatus, out int status);

            if (status != (int)GLEnum.True)
                throw new Exception($"Fragment shader failed to compile: {gl.GetShaderInfoLog(Handle)}");
        }
    }
}
