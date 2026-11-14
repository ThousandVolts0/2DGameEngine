using Conduit.Engine.Content;

namespace Conduit.Engine.Rendering.Shapes
{
    public class Quad : IRenderable
    {
        public Mesh Mesh => Mesh.Quad;
        public Material? Material { get; }

        public Quad(Material? material = null)
        {
            Material = material;
        }
    }
}
