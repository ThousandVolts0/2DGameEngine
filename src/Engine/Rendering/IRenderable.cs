using Engine.Content;
using Engine.Data;

namespace Engine.Rendering
{
    public interface IRenderable
    {
        public Material? Material { get; }
        public Mesh Mesh { get; }
    }
}
