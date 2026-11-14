using Conduit.Engine.Content;

namespace Conduit.Engine.Rendering
{
    public interface IRenderable
    {
        public Material? Material { get; }
        public Mesh Mesh { get; }
    }
}
