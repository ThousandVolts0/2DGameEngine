using Conduit.Engine.Content;

namespace Conduit.Engine.Rendering.Shapes
{
    public class Circle : IRenderable
    {
        public Mesh Mesh => Mesh.Circle;
        public Material? Material { get; }

        public Circle(Material? material = null)
        {
            Material = material;
        }
    }
}
