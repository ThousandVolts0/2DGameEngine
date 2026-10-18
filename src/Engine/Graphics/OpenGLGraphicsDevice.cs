using Silk.NET.OpenGL;
using Silk.NET.Maths;
using System.Drawing;
using Engine.Rendering;
using Engine.Data;
using Engine.Content;

namespace Engine.Graphics
{
    public class OpenGLGraphicsDevice : IGraphicsDevice 
    {
        public OpenGLGraphicsDevice() { }
        public void Initialize(object gl) => _gl = (GL)gl;

        private Dictionary<Mesh, GPUMesh> _loadedMeshes = new();
        private GL _gl = null!;

        private GPUMesh LoadMesh(Mesh data)
        {
            uint vao = CreateVAO();
            uint vbo = CreateVBO(data);
            uint ebo = CreateEBO(data);

            const uint posAttrib = 0;
            _gl.EnableVertexAttribArray(posAttrib);
            _gl.VertexAttribPointer(posAttrib, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0); // 2D

            _loadedMeshes[data] = new()
            {
                VAO = vao,
                VBO = vbo,
                EBO = ebo,
            };

            return _loadedMeshes[data];
        }

        public unsafe void Draw(Mesh data, Material material, Matrix4X4<float> proj)
        {
            GPUMesh mesh = _loadedMeshes.ContainsKey(data) ? _loadedMeshes[data] : LoadMesh(data);

            material.Program.Use();
            material.Program.SetUniform("projection", proj);

            _gl.BindVertexArray(mesh.VAO);
            _gl.DrawElements(PrimitiveType.Triangles, (uint)data.Indices.Length, DrawElementsType.UnsignedInt, null);
        }

        public void SetViewport(Viewport view)
        {
            _gl.Viewport((int)view.X, (int)view.Y, (uint)view.Width, (uint)view.Height);
        }

        public void Clear()
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void SetClearColor(Color color)
        {
            _gl.ClearColor(color);
        }

        private uint CreateVAO()
        {
            var vao = _gl.GenVertexArray();
            _gl.BindVertexArray(vao);
            return vao;
        }

        private unsafe uint CreateVBO(Mesh mesh)
        {
            var vbo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

            fixed (float* buf = mesh.Vertices)
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(mesh.Vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);

            return vbo;
        }

        private unsafe uint CreateEBO(Mesh mesh)
        {
            var ebo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);

            fixed (uint* buf = mesh.Indices)
                _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(mesh.Indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

            return ebo; 
        }

        private uint CreateShader(ShaderType type, string source)
        {
            uint shader = _gl.CreateShader(type);
            _gl.ShaderSource(shader, source);
            _gl.CompileShader(shader);
            _gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
            if (status != (int)GLEnum.True)
                throw new Exception($"{type} shader failed to compile: " + _gl.GetShaderInfoLog(shader));
            return shader;
        }
    }
}
