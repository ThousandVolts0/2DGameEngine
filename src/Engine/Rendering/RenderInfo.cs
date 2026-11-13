using System.Numerics;

namespace Engine.Rendering
{
    public class RenderInfo
    {
        public Matrix4x4 World { get; }
        public Matrix4x4 View { get; }
        public Matrix4x4 Projection { get; }

        public RenderInfo(Matrix4x4 world, Matrix4x4 view, Matrix4x4 proj)
        {
            World = world;
            View = view;
            Projection = proj;
        }
    }
}
