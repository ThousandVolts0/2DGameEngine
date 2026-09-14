namespace Engine.Core
{
    public class Scene
    {
        private Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        public Entity? GetEntityByName(string name) => _entities.Values.FirstOrDefault(e => e.Name == name);
        public Entity? GetEntityById(int id) => _entities.ContainsKey(id) ? _entities[id] : null;
        public IEnumerable<Entity> GetEntities() => _entities.Values;

        public void AddEntity(Entity entity) => _entities.Add(_entities.Count, entity);
        public void RemoveEntity(Entity entity) => _entities.Remove(_entities.FirstOrDefault(e => e.Value == entity).Key);
    }
}
