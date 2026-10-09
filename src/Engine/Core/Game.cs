using Engine.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Drawing;
using Silk.NET.Windowing;
using Engine.Graphics;

namespace Runtime
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
            Services.RenderPipeline.Render(Services.Worlds.ActiveWorld);
            Services.Window.SwapBuffers();
        }

        public void OnClose()
        {

        }

        public void OnResize(Vector2D<int> newSize)
        {
            
        }
    }
}
