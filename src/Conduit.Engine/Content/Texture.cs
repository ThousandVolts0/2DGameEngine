namespace Conduit.Engine.Content
{
    public class Texture
    {
        public required byte[] ImagePixels { get; init; }

        public required int Width { get; init; }
        public required int Height { get; init; }
    }
}
