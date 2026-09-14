using Silk.NET.OpenGL;
using Silk.NET.Maths;
using Engine.Components;
using Engine.Core;

namespace Engine.Rendering
{
    public class RenderPipeline
    {
        private GL _gl;
        private uint _program;
        public RenderPipeline(GL gl, uint program)
        {
            _gl = gl;
            _program = program;
        }

        public unsafe void RenderScene(Scene scene, Viewport view, Camera cam)
        {
            foreach (Entity entity in scene.GetEntities().Where(e => e.HasComponent<Renderer>()))
            {
                var renderer = entity.GetComponent<Renderer>();

                if (renderer != null && renderer.Object != null)
                    RenderObject(renderer.Object, cam.GetProjectionMatrix(view));
            }
        }

        public unsafe void RenderObject(RenderObject obj, Matrix4X4<float> projection)
        {
            uint vao = CreateVAO(obj);
            uint vbo = CreateVBO(obj);
            uint ebo = CreateEBO(obj);
            
            uint vertexShader = CreateShader(ShaderType.VertexShader, obj.Data.VertexShader);
            uint fragmentShader = CreateShader(ShaderType.FragmentShader, obj.Data.FragmentShader);

            _gl.AttachShader(_program, vertexShader);
            _gl.AttachShader(_program, fragmentShader);

            _gl.LinkProgram(_program);

            _gl.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);

            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(_program));

            _gl.DetachShader(_program, vertexShader);
            _gl.DetachShader(_program, fragmentShader);
            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);

            _gl.UseProgram(_program);

            const uint pos = 0;
            _gl.EnableVertexAttribArray(pos);
            _gl.VertexAttribPointer(pos, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            int projectionLoc = _gl.GetUniformLocation(_program, "projection");
            _gl.UniformMatrix4(projectionLoc, 1, false, (float*)&projection);

            _gl.BindVertexArray(0);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

            _gl.BindVertexArray(vao);
            _gl.DrawElements(PrimitiveType.Triangles, (uint)obj.Data.Indices.Length, DrawElementsType.UnsignedInt, null);
        }

        private uint CreateVAO(RenderObject obj)
        {
            var vao = _gl.GenVertexArray();
            _gl.BindVertexArray(vao);
            return vao;
        }

        private unsafe uint CreateVBO(RenderObject obj)
        {
            var vbo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

            fixed (float* buf = obj.Data.Vertices)
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(obj.Data.Vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);

            return vbo;
        }

        private unsafe uint CreateEBO(RenderObject obj)
        {
            var ebo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);

            fixed (uint* buf = obj.Data.Indices)
                _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(obj.Data.Indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

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
