using Engine.Content;
using Engine.Data;
using Engine.Rendering;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

namespace Engine.Graphics
{
    public interface IGraphicsDevice
    {
        void SetClearColor(Color color);
        void Clear();
        void Initialize(IWindow window);
        void SetViewport(Viewport view);

        unsafe void Draw(MeshRenderInfo info, Matrix4X4<float> proj);
    }
}
