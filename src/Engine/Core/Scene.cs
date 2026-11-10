using Engine.Components;
using Engine.Core.Game.Abstractions;

namespace Engine.Core
{
    public abstract class Scene : IDisposable
    {
        private Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        private int _nextEntityId = 0;
        private bool _isInitialized = false;

        public Scene() { }

        public Scene(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                AddEntity(entity);
        }

        protected virtual void OnInitialize(GameContext ctx) { }
        protected virtual void OnActivate(GameContext ctx) { }
        protected virtual void OnDeactivate(GameContext ctx) { }

        internal void Initialize(GameContext ctx)
        {
            if (_isInitialized) return; // Prevent re-initialization

            OnInitialize(ctx);
            _isInitialized = true;
        }

        internal void Activate(GameContext ctx) => OnActivate(ctx);
        internal void Deactivate(GameContext ctx) => OnDeactivate(ctx);

        public Entity? GetEntityByName(string name) => _entities.Values.FirstOrDefault(e => e.Name == name);
        public Entity? GetEntityById(int id) => _entities.ContainsKey(id) ? _entities[id] : null;
        public IReadOnlyList<Entity> GetEntities() => _entities.Values.ToList().AsReadOnly();
        public IReadOnlyList<Component> GetComponentsInEntities()
        {
            List<Component> components = new();
            foreach (Entity entity in GetEntities())
                components.AddRange(entity.GetComponents());

            return components.AsReadOnly();
        }
        public IReadOnlyList<T> GetComponentsOfTypeInEntities<T>() where T : Component
        {
            List<T> components = new();
            foreach (Entity entity in GetEntities())
                components.AddRange(entity.GetComponentsOfType<T>());
            return components.AsReadOnly();
        }

        public void AddEntity(Entity entity) => _entities.Add(_nextEntityId++, entity);

        public void RemoveEntity(Entity entity) => _entities.Remove(_entities.First(e => e.Value == entity).Key);

        public void ClearEntities()
        {
            _entities.Clear();
            _nextEntityId = 0;
            _isInitialized = false;
        }

        public void Dispose()
        {
            ClearEntities();
        }
    }
}
