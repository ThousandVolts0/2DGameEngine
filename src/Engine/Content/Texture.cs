using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Content
{
    public class Texture
    {
        public required byte[] ImagePixels { get; init; }

        public required int Width { get; init; }
        public required int Height { get; init; }
    }
}
