using Engine;
using Engine.Components;
using Engine.Rendering;
using Engine.Core;
using Microsoft.Extensions.Options;
using System.Numerics;
using Engine.Core.Game;
using Engine.Mathematics;
using Vector2 = Engine.Mathematics.Vector2;

namespace TestGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Element player = new Element("Player")
                .Attach<Transform>();

            Canvas canvas = new Canvas("Main")
                .AddElement(player);

            Game game = Game.Create()
                .Window(800, 600, "Test game")
                .Build();
        }
    }
}
