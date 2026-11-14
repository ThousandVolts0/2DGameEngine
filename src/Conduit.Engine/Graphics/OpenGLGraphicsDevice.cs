using Silk.NET.OpenGL;
using System.Drawing;
using Silk.NET.Windowing;
using Microsoft.Extensions.Logging;
using Conduit.Engine.Graphics.Shaders;
using Conduit.Engine.Content;
using Conduit.Engine.Rendering;

namespace Conduit.Engine.Graphics
{
    public class OpenGLGraphicsDevice : IGraphicsDevice 
    {
        private readonly ILogger<OpenGLGraphicsDevice> _logger;
        private readonly Dictionary<Mesh, GPUMesh> _loadedMeshes = new();
        private GL _gl = null!;

        public OpenGLGraphicsDevice(ILogger<OpenGLGraphicsDevice> logger)
        {
            _logger = logger;
        }

        private GPUMesh LoadMesh(Mesh data)
        {
            uint vao = CreateVAO();
            uint vbo = CreateVBO(data);
            uint ebo = CreateEBO(data);

            const uint posAttrib = 0;
            _gl.EnableVertexAttribArray(posAttrib);
            _gl.VertexAttribPointer(posAttrib, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0); 

            _loadedMeshes[data] = new()
            {
                VAO = vao,
                VBO = vbo,
                EBO = ebo,
            };

            return _loadedMeshes[data];
        }

        public void Draw(Mesh mesh, Material material, RenderInfo info)
        {
            _logger.LogDebug("Drawing mesh: {Mesh} with material {Material}", mesh, material);

            GPUMesh gpuMesh = _loadedMeshes.ContainsKey(mesh) ? _loadedMeshes[mesh] : LoadMesh(mesh);
            IShader shader = material.Shader;

            shader.Bind();
            shader.SetUniform("u_Projection", info.Projection);
            shader.SetUniform("u_Model", info.Model);
            shader.SetUniform("u_View", info.View);

            material.ApplyProperties();

            _gl.BindVertexArray(gpuMesh.VAO);
            _gl.DrawElements(PrimitiveType.Triangles, (uint)mesh.Indices.Length, DrawElementsType.UnsignedInt, ReadOnlySpan<uint>.Empty);
        }

        public IShader CreateShader(ShaderSource source)
        {
            uint vert = CreateShader(ShaderType.VertexShader, source.Vertex);
            uint frag = CreateShader(ShaderType.FragmentShader, source.Fragment);
            return new OpenGLShader(_gl, frag, vert);
        }

        public void SetViewport(Viewport view)
        {
            _gl.Viewport((int)view.X, (int)view.Y, (uint)view.Width, (uint)view.Height);
        }

        public void Initialize(IWindow window) => _gl = window.CreateOpenGL();
        public void Clear() => _gl.Clear(ClearBufferMask.ColorBufferBit);
        public void SetClearColor(Color color) => _gl.ClearColor(color);

        public void Dispose()
        {
            foreach (GPUMesh mesh in _loadedMeshes.Values)
            {
                _gl.DeleteVertexArray(mesh.VAO);
                _gl.DeleteBuffer(mesh.VBO);
                _gl.DeleteBuffer(mesh.EBO);
            }

            _loadedMeshes.Clear();
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
