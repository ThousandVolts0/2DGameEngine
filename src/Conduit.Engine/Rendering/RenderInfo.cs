using System.Numerics;

namespace Conduit.Engine.Rendering
{
    public class RenderInfo
    {
        public Matrix4x4 Model { get; }
        public Matrix4x4 View { get; }
        public Matrix4x4 Projection { get; }

        public RenderInfo(Matrix4x4 model, Matrix4x4 view, Matrix4x4 proj)
        {
            Model = model;
            View = view;
            Projection = proj;
        }
    }
}
