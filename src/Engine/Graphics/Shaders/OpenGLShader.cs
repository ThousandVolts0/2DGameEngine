using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Diagnostics;
using System.Numerics;
using System.Transactions;

namespace Engine.Graphics.Shaders
{
    public class OpenGLShader : IShader
    {
        public uint Handle { get; }
        private Dictionary<string, int> _uniformLocations = new();
        private GL _gl;
        internal OpenGLShader(GL gl, uint frag, uint vert)
        {
            _gl = gl;
            Handle = _gl.CreateProgram();
            LinkShaders(frag, vert);
        }

        private void LinkShaders(uint frag, uint vert)
        {
            _gl.AttachShader(Handle, vert);
            _gl.AttachShader(Handle, frag);

            _gl.LinkProgram(Handle);
            _gl.GetProgram(Handle, ProgramPropertyARB.LinkStatus, out int lStatus);

            if (lStatus != (int)GLEnum.True)
                throw new Exception($"Program failed to link: {_gl.GetProgramInfoLog(Handle)}");

            _gl.DetachShader(Handle, vert);
            _gl.DetachShader(Handle, frag);
            _gl.DeleteShader(vert);
            _gl.DeleteShader(frag);
        }

        private int GetUniformLocation(string name)
        {
            if (_uniformLocations.TryGetValue(name, out int value))
                return value;

            int loc = _gl.GetUniformLocation(Handle, name);
            _uniformLocations[name] = loc;

            return loc;
        }

        public void Bind()
        {
            _gl.UseProgram(Handle);
        }

        public void Dispose() => _gl.DeleteProgram(Handle);

        public void SetUniform<T>(string name, T value)
        {
            switch (value)
            {
                case int i:
                    _gl.Uniform1(GetUniformLocation(name), i);
                    break;
                case float f:
                    _gl.Uniform1(GetUniformLocation(name), f);
                    break;
                case bool b:
                    _gl.Uniform1(GetUniformLocation(name), b ? 1 : 0);
                    break;
                case Vector2 v2:
                    _gl.Uniform2(GetUniformLocation(name), v2);
                    break;
                case Vector3 v3:
                    _gl.Uniform3(GetUniformLocation(name), v3);
                    break;
                case Vector4 v4:
                    _gl.Uniform4(GetUniformLocation(name), v4);
                    break;
                case Matrix4x4 m4:
                    _gl.UniformMatrix4(GetUniformLocation(name), 1, true, ref m4.M11);
                    break;
                default:
                    throw new Exception($"Unsupported property type: {value?.GetType()}");
            }
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }
}
