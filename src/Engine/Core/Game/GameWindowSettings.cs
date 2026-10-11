using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Game
{
    public class GameWindowSettings
    {
        public int Width { get; set; } = 1000;
        public int Height { get; set; } = 600;
        public bool UseVSync { get; set; } = true;
        public string Title { get; set; } = "New Game";
    }
}
