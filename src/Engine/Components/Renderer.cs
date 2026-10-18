using Engine.Content;
using Engine.Core;
using Engine.Data;

namespace Engine.Components
{
    public sealed class Renderer : Component
    {
        public Mesh Mesh { get; }
        public Material Material { get; }
        public Renderer(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
}
