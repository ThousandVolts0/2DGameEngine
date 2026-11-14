using Conduit.Engine.Rendering;

namespace Conduit.Engine.Components
{
    public sealed class Renderer : Component
    {
        public IRenderable Renderable { get; }
        public Renderer(IRenderable renderable)
        {
            Renderable = renderable;
        }
    }
}
