using Conduit.Engine.Content;

namespace Conduit.Engine.Rendering.Shapes
{
    public class Triangle : IRenderable
    {
        public Mesh Mesh => Mesh.Triangle;
        public Material? Material { get; }

        public Triangle(Material? material = null)
        {
            Material = material;
        }
    }
}
