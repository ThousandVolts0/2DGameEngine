using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Graphics.Shaders
{
    public class ShaderSource
    {
        public required string Vertex { get; init; }
        public required string Fragment { get; init; }
    }
}
