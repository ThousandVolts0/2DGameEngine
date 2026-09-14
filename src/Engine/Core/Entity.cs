using Engine.Components;

namespace Engine.Core
{
    public class Entity
    {
        public string Name { get; set; } = "Entity";

        private List<IComponent> _components = new List<IComponent>();

        public Entity(string name)
        {
            Name = name;
        }

        public Entity(string name, params IComponent[] components)
        {
            Name = name;
            _components = components.ToList();
        }

        public Entity() { }

        public void AddComponent(IComponent component)
        {
            Type type = component.GetType();
            if (HasComponent(type))
                throw new InvalidOperationException($"Component of type {type.Name} already exists in entity {Name}.");

            _components.Add(component);
        }

        public void AddComponent<T>() where T : IComponent, new()
        {
           if (HasComponent<T>())
                throw new InvalidOperationException($"Component of type {typeof(T).Name} already exists in entity {Name}.");

            _components.Add(new T());
        }

        public bool HasComponent<T>() where T : IComponent => _components.Any(c => c.GetType() == typeof(T));
        public bool HasComponent(Type type)  => _components.Any(c => c.GetType() == type);

        public void RemoveComponent(IComponent component) => _components.Remove(component); 
        public void RemoveComponent<T>() where T : IComponent
        {
            var component = _components.FirstOrDefault(c => c is T);
            if (component != null)
                _components.Remove(component);
        }

        public T? GetComponent<T>() where T : IComponent => _components.OfType<T>().FirstOrDefault();
    }
}
