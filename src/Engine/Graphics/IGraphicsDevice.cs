using Engine.Content;
using Engine.Data;
using Engine.Rendering;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Drawing;

namespace Engine.Graphics
{
    public interface IGraphicsDevice
    {
        void SetClearColor(Color color);
        void Clear();
        void Initialize(object context);
        void SetViewport(Viewport view);

        unsafe void Draw(Mesh mesh, Material material, Matrix4X4<float> proj);
    }
}
