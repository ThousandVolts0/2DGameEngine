using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Engine.Editor
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
            services.AddSingleton<EditorWindow>();
            services.AddSingleton<IWindow>(window);

            var serviceProvider = services.BuildServiceProvider();
            var editorWindow = serviceProvider.GetRequiredService<EditorWindow>();

            window.Load += editorWindow.OnLoad;
            window.Update += editorWindow.OnUpdate;
            window.Render += editorWindow.OnRender;
            window.Closing += editorWindow.OnClosing;
            window.Resize += editorWindow.OnResize;

            window.Run();
            window.Dispose();
        }
    }
}
