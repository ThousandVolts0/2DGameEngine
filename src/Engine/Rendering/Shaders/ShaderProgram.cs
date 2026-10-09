using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;
using System.Transactions;

namespace Engine.Rendering.Shaders
{
    public class ShaderProgram
    {
        public uint Handle { get; }
        private Dictionary<string, int> _uniformLocations = new();
        private GL _gl;
        public ShaderProgram(GL gl, FragmentShader frag, VertexShader vert)
        {
            _gl = gl;
            Handle = _gl.CreateProgram();
            _gl.AttachShader(Handle, vert.Handle);
            _gl.AttachShader(Handle, frag.Handle);

            _gl.LinkProgram(Handle);
            _gl.GetProgram(Handle, ProgramPropertyARB.LinkStatus, out int lStatus);

            if (lStatus != (int)GLEnum.True)
                throw new Exception($"Program failed to link: {_gl.GetProgramInfoLog(Handle)}");

            _gl.DetachShader(Handle, vert.Handle);
            _gl.DetachShader(Handle, frag.Handle);
            _gl.DeleteShader(vert.Handle);
            _gl.DeleteShader(frag.Handle);
        }

        public int GetUniformLocation(string name)
        {
            if (_uniformLocations.TryGetValue(name, out int value))
                return value;

            int loc = _gl.GetUniformLocation(Handle, name);
            _uniformLocations[name] = loc;

            return loc;
        }

        public void Use()
        {
            _gl.UseProgram(Handle);
        }

        public void Dispose() => _gl.DeleteProgram(Handle);

        public void SetUniform(string name, bool value) => _gl.Uniform1(GetUniformLocation(name), value ? 1 : 0);

        public void SetUniform(string name, int value) => _gl.Uniform1(GetUniformLocation(name), value);

        public void SetUniform(string name, float value) => _gl.Uniform1(GetUniformLocation(name), value);

        public void SetUniform(string name, Vector2 value) => _gl.Uniform2(GetUniformLocation(name), value);

        public void SetUniform(string name, Vector3 value) => _gl.Uniform3(GetUniformLocation(name), value);

        public unsafe void SetUniform(string name, Matrix4X4<float> value) => _gl.UniformMatrix4(GetUniformLocation(name), 1, false, (float*)&value);

        public override string ToString()
        {
            return Handle.ToString();
        }
    }
}
