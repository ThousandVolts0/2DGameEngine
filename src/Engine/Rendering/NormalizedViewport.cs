using System.Drawing;
namespace Engine.Rendering
{
    // Represents a rectangular part of the screen using normalized coordinates (0.0 to 1.0) for X and Y, and width and height as fractions of the total screen size.
    public struct NormalizedViewport
    {
        public float X { get; }
        public float Y { get; }

        public float Width { get; }
        public float Height { get; }

        public NormalizedViewport(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static NormalizedViewport FullScreen => new NormalizedViewport(0f, 0f, 1f, 1f);
        public static Viewport operator *(NormalizedViewport viewport, Size size)
        {
            return new Viewport(
                viewport.X * size.Width,
                viewport.Y * size.Height,
                viewport.Width * size.Width,
                viewport.Height * size.Height
            );
        }
    }
}
