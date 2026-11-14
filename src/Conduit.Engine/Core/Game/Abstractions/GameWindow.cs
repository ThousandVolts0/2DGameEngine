using Conduit.Engine.Mathematics;
using Microsoft.Extensions.Logging;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Conduit.Engine.Core.Game.Abstractions
{
    public class GameWindow : IDisposable
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

        private readonly IWindow _window;
        private readonly ILogger<GameWindow> _logger;
        public GameWindow(IWindow window, ILogger<GameWindow> logger)
        {
            _window = window;
            _logger = logger;

            _window.Move += HandleMove;
            _window.Resize += HandleResize;
        }

        private void HandleMove(Vector2D<int> pos) => OnMove?.Invoke((Vector2Int)pos);
        private void HandleResize(Vector2D<int> size) => OnResize?.Invoke((Vector2Int)size);

        public void Dispose()
        {
            _window.Move -= HandleMove;
            _window.Resize -= HandleResize;
        }
    }
}
