using Conduit.Engine.Content;

namespace Conduit.Engine.Rendering
{
    public class Sprite : IRenderable
    {
        public Mesh Mesh => Mesh.Quad;
        public Material Material { get; }

        public Sprite(Material material)
        {
            Material = material;
        }
    }
}
