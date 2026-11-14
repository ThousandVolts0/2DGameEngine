using Conduit.Engine.Core.Game.Abstractions;

namespace Conduit.Engine.Components
{
    public class Behaviour : Component
    {
        private GameContext? _context = null;
        protected GameContext Context => _context
            ?? throw new InvalidOperationException("Game context hasn't been initialized yet, don't run gameplay code inside constructor");

        internal void InitializeContext(GameContext context)
        {
            _context = context;
        }
    }
}
