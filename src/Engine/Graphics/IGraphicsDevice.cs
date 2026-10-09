using Engine.Rendering;
using System.Drawing;
using Silk.NET.Maths;
using Engine.Data;
using Engine.Content;

namespace Engine.Graphics
{
    public interface IGraphicsDevice
    {
        void Clear(Color color);
        void Initialize(object context);
        void SetViewport(Viewport view);

        unsafe void Draw(Mesh mesh, Material material, Matrix4X4<float> proj);
    }
}
