using Conduit.Engine.Content;
using Conduit.Engine.Graphics.Shaders;
using Conduit.Engine.Rendering;
using Silk.NET.Windowing;
using System.Drawing;

namespace Conduit.Engine.Graphics
{
    public interface IGraphicsDevice : IDisposable
    {
        void SetClearColor(Color color);
        void Clear();
        void Initialize(IWindow window);
        void SetViewport(Viewport view);
        IShader CreateShader(ShaderSource source);

        void Draw(Mesh mesh, Material material, RenderInfo info);
    }
}
