using Engine;
using Engine.Components;
using Engine.Content;
using Engine.Core;
using Engine.Core.Game;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace TestGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Entity player = new Entity("Player")
                .Attach(new Transform());

            Scene main = new Scene("Main")
                .AddEntity(player);

            Game game = Game.Create()
                .WindowOptions(options =>
                {
                    options.Height = 600;
                    options.Width = 1000;
                    options.Title = "Sample game";
                })
                .Logging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.AddConsole();
                })
                .StartingScene(main)
                .Build();

            game.Run();
        }
    }
}
