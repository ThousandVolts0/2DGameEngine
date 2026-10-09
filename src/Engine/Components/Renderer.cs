using Engine.Content;
using Engine.Data;

namespace Engine.Components
{
    public class Renderer : IComponent
    {
        public Mesh MeshData { get; set; }
        public MaterialData Material { get; set; }
    }
}
