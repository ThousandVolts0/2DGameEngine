using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using Engine.Graphics;

namespace Engine.Core.Game
{
    public class Game
    {
        public GameSettings Settings { get; }
        public GameServices Services { get; }

        public double FPS => Services.Window.FramesPerSecond;

        internal Game(GameSettings settings, GameServices services)
        {
            Settings = settings;
            Services = services;
        }

        public void Run()
        {
            var window = Services.Window;

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Render += OnRender;
            window.Closing += OnClose;
            window.Resize += OnResize;

            window.Run();
            window.Dispose();
        }

        public void OnLoad()
        {
            IGraphicsDevice graphics = Services.Graphics;

            graphics.Initialize(Services.Window.CreateOpenGL());
            graphics.Clear(Color.AliceBlue);
        }

        public void OnUpdate(double dt)
        {

        }

        public void OnRender(double dt)
        {
            Services.Graphics.Clear(Color.AliceBlue);
            Services.RenderPipeline.Render(Services.Worlds.ActiveCanvas);
            Services.Window.SwapBuffers();
        }

        public void OnClose()
        {

        }

        public void OnResize(Vector2D<int> newSize)
        {
            
        }

        public static GameBuilder Create() => new GameBuilder();
    }

}
