using Silk.NET.Windowing;
using Silk.NET.Maths;

namespace Runtime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var windowOptions = WindowOptions.Default with
            {
                API = GraphicsAPI.Default,
                Size = new Vector2D<int>(1000, 600),
                Title = "Game Engine",
                VSync = true
            };

            var window = Window.Create(windowOptions);
            var gameWindow = new GameWindow(window);

            window.Load += gameWindow.OnLoad;
            window.Update += gameWindow.OnUpdate;
            window.Render += gameWindow.OnRender;
            window.Closing += gameWindow.OnClosing;
            window.Resize += gameWindow.OnResize;

            window.Run();
            window.Dispose();
        }
    }
}
