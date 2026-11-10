using Silk.NET.Maths;
using Engine.Rendering;
using System.Drawing;
using Engine.Core;
using System.Numerics;

namespace Engine.Components
{
    public sealed class Camera : Component
    {
        public float OrthographicSize { get; set; } = 1.0f;
        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 100.0f;

        public NormalizedViewport ViewRectangle { get; set; } = NormalizedViewport.FullScreen;

        public Matrix4x4 GetProjectionMatrix(Size windowSize)
        {
            Viewport view = ViewRectangle * windowSize;
            float aspectRatio = (float)view.Width / view.Height;
            float left = -aspectRatio * OrthographicSize;
            float right = aspectRatio * OrthographicSize;
            float bottom = -OrthographicSize;
            float top = OrthographicSize;

            return Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, NearPlane, FarPlane);
        }
    }
}
