using Engine.Components;

namespace Engine.Core
{
    public class Scene
    {
        public string Name { get; set; } = "Scene";

        private Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        private int _nextEntityId = 0;

        public Scene(string name = "Scene")
        {
            Name = name;
        }

        public Scene(List<Entity> entities, string name = "Scene") : this(name)
        {
            foreach (Entity entity in entities)
                AddEntity(entity);
        }

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

        public Scene AddEntity(Entity entity)
        {
            _entities.Add(_nextEntityId++, entity);
            return this;
        }

        public Scene RemoveEntity(Entity entity)
        {
            _entities.Remove(_entities.First(e => e.Value == entity).Key);
            return this;
        }
    }
}
