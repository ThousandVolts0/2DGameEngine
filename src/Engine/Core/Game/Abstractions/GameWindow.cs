using Engine.Mathematics;
using ImGuiNET;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Engine.Core.Game.Abstractions
{
    public class GameWindow
    {
        public int Width => _window.Size.X;
        public int Height => _window.Size.Y;

        public int X => _window.Position.X;
        public int Y => _window.Position.Y;

        public event Action<Vector2Int>? OnResize;
        public event Action<Vector2Int>? OnMove;

        public WindowState WindowState => _window.WindowState;
        public double FPS => _window.FramesPerSecond;

        public void SetPosition(Vector2Int newPos) => _window.Position = (Vector2D<int>)newPos;
        public void SetSize(Vector2Int newSize) => _window.Size = (Vector2D<int>)newSize;
        public void SetWindowState(WindowState newState) => _window.WindowState = newState;

        private IWindow _window;
        public GameWindow(IWindow window)
        {
            _window = window;
            _window.Move += (newPos) => OnMove?.Invoke((Vector2Int)newPos);
            _window.Resize += (newSize) => OnResize?.Invoke((Vector2Int)newSize);
        }
    }
}
