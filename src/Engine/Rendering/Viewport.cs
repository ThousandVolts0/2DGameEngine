using System.Drawing;
namespace Engine.Rendering
{
    // Represents a rectangular part of the screen using screen pixel coordinates
    public struct Viewport
    {
        public float X { get; }
        public float Y { get; }

        public float Width { get; }
        public float Height { get; }

        public Viewport(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
