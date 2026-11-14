using Conduit.Engine.Components;
using Conduit.Engine.Core;
using Conduit.Engine.Core.Game;
using Conduit.Engine.Core.Game.Abstractions;
using Conduit.Engine.Rendering.Shapes;
using Microsoft.Extensions.Logging;
using System.Numerics;
using Vector2 = Conduit.Engine.Mathematics.Vector2;

namespace Conduit.Samples.BasicGame
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
                .AddScene<MainScene>("Main")
                .SetActiveScene("Main")
                .Build();

            game.Run();
        }
    }

    public class MainScene : Scene
    {
        protected override void OnInitialize(GameContext ctx)
        {
            // Todo: maybe remove engine Vector2 and replace with
            // System.Numerics.Vector2 for consistency

            // Todo: maybe make scene manager hold scene type and
            // when u load it creates instance and calls OnInitialize

            var material = ctx.Resources.Defaults.Materials.Unlit;
            material.Color = new Vector4(1f, 1f, 1f, 1f);
            var playerShape = new Triangle(material);

            Vector2 pos = new(0, 0f);
            Vector2 scale = new(1f, 1f);
            float rot = 0f;

            Entity mainCam = new Entity("MainCamera")
                .Attach(new Camera());

            Entity player = new Entity("Player")
                .Attach(new Transform(pos, scale, rot))
                .Attach(new Renderer(playerShape));

            AddEntity(player);
            AddEntity(mainCam);
        }
    }
}
