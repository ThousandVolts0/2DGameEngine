using Engine;
using Engine.Components;
using Engine.Content;
using Engine.Core;
using Engine.Core.Game;
using Engine.Core.Game.Abstractions;
using Engine.Rendering;
using Engine.Rendering.Shapes;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace TestGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new GameBuilder()
                .Configure((settings) =>
                {
                    settings.Window.Width = 1000;
                    settings.Window.Height = 600;
                    settings.Window.Title = "Sample game";

                    settings.Logging.MinimumLevel = LogLevel.Debug;
                    settings.Logging.UseConsole = true;
                })
                .RegisterScene(new MainScene(), "Main")
                .SetActiveScene("Main")
                .Build();

            game.Run();
        }
    }

    public class MainScene : Scene
    {
        protected override void OnInitialize(GameContext ctx)
        {
            var playerShape = new Circle();

            Entity player = new Entity("Player")
                .Attach(new Transform())
                .Attach(new Renderer(playerShape))
                .Attach(new Camera());

            AddEntity(player);
        }
    }
}
