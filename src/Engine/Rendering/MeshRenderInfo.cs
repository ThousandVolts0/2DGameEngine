using Engine.Content;
using Engine.Data;

namespace Engine.Rendering
{
    public class MeshRenderInfo
    {
        public Material Material { get; }
        public Mesh Mesh { get; }

        public MeshRenderInfo(Mesh mesh, Material material)
        {
            Mesh = mesh;
            Material = material;
        }
    }
}
