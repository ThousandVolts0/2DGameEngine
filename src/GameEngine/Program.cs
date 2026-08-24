using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;

namespace GameEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            var windowOptions = WindowOptions.Default with
            {
                API = GraphicsAPI.Default,
                Size = new Vector2D<int>(1000, 600),
                Title = "Game Engine",
                VSync = true
            };

            var window = Window.Create(windowOptions);

            services.AddLogging(config => config.AddConsole());
            services.AddSingleton<Engine>();
            services.AddSingleton<IWindow>(window);

            var serviceProvider = services.BuildServiceProvider();
            var engine = serviceProvider.GetRequiredService<Engine>();

            window.Load += engine.OnLoad;
            window.Update += engine.OnUpdate;
            window.Render += engine.OnRender;
            window.Closing += engine.OnClosing;
            window.Resize += engine.OnResize;

            window.Run();
            window.Dispose();
        }
    }
}
