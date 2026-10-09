using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public class WorldRegistry
    {
        private List<World> _worlds = new List<World>();
        public World ActiveWorld { get; private set; } = null!;
        public void SetActiveWorld(World world)
        {
            if (!_worlds.Contains(world))
                throw new InvalidOperationException("World is not registered in the registry.");

            ActiveWorld = world;
        }

        public IReadOnlyList<World> GetWorlds() => _worlds.AsReadOnly();
        public void AddWorld(World world) => _worlds.Add(world);
        public void RemoveWorld(World world) => _worlds.Remove(world);
    }
}
