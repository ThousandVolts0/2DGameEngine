using Engine.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Runtime;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.OpenGL;
using System.Drawing;
using Engine.Graphics;

namespace Engine.Core
{
    public class GameBuilder
    {
        private WindowOptions _windowOptions = WindowOptions.Default;

        public GameBuilder Window(int width, int height, string title = "New Game")
        {
            _windowOptions.Size = new Vector2D<int>(width, height);
            _windowOptions.Title = title;
            return this;
        }

        public Game Build()
        {
            var services = new ServiceCollection();

            services.AddSingleton(Silk.NET.Windowing.Window.Create(_windowOptions));
            services.AddSingleton<WorldRegistry>();
            services.AddSingleton<OpenGLGraphicsDevice>();
            services.AddSingleton<RenderPipeline>();

            GameServices gameServices = new GameServices(services.BuildServiceProvider());

            Game game = new Game(new GameSettings(), gameServices);
            return game;
        }
    }
}
