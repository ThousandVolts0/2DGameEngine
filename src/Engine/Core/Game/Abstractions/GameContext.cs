using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Game.Abstractions
{
    public class GameContext
    {
        public ILogger<Game> Logger { get; }
        public SceneManager Scenes { get; }
        public GameInput Input { get; }
        public GameWindow Window { get; }

        public GameContext(
            ILogger<Game> logger, 
            SceneManager sceneManager, 
            GameInput input,
            GameWindow window)
        {
            Logger = logger;
            Scenes = sceneManager;
            Input = input;
            Window = window;
        }
    }
}
