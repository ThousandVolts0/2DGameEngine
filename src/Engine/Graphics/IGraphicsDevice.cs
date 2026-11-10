using Engine.Content;
using Engine.Data;
using Engine.Graphics.Shaders;
using Engine.Rendering;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

namespace Engine.Graphics
{
    public interface IGraphicsDevice
    {
        void SetClearColor(Color color);
        void Clear();
        void Initialize(IWindow window);
        void SetViewport(Viewport view);
        IShader CreateShader(ShaderSource source);

        void Draw(Mesh mesh, Material material, Matrix4x4 proj);
    }
}
