using Engine.Content;
using Engine.Core;
using Engine.Data;
using Engine.Rendering;

namespace Engine.Components
{
    public sealed class Renderer : Component
    {
        public MeshRenderInfo Info { get; }
        public Renderer(MeshRenderInfo info)
        {
            Info = info;
        }
    }
}
