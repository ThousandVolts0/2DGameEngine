using Engine.Mathematics;
using Silk.NET.Maths;
using System;

namespace Runtime
{
    public class GameSettings
    {
        public int Width { get; set; } = 1000;
        public int Height { get; set; } = 600;
        public bool UseVSync { get; set; } = true;
        public string Title { get; set; } = "New Game";
    }
}
