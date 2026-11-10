using Engine.Content;
using Engine.Core;
using Engine.Data;
using Engine.Rendering;

namespace Engine.Components
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
