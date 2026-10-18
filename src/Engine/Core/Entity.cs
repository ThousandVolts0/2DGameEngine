using Engine.Components;

namespace Engine.Core
{
    public class Entity
    {
        public string Name { get; set; } = "Entity";

        private List<Component> _components = new List<Component>();

        public Entity(string name = "Entity")
        {
            Name = name;
        }

        public Entity(string name, params Component[] components) : this(name)
        {
            _components = components.ToList();
        }

        public Entity(params Component[] components) : this("Entity", components) { }

        public Entity Attach(Component component)
        {
            Type type = component.GetType();
            if (HasComponent(type))
                throw new InvalidOperationException($"Component of type {type.Name} already exists in entity {Name}.");

            if (component.IsAttachedToEntity)
                throw new InvalidOperationException("Component is already attached to an existing entity, detach first");

            _components.Add(component);
            component.AttachToEntity(this);
            return this;
        }

        public Entity Detach(Component component)
        {
            bool removed = _components.Remove(component);
            if (!removed)
                throw new InvalidOperationException($"Component of type {component.GetType().Name} does not exist in entity {Name}.");

            component.DetachFromEntity();

            return this;
        }

        public Entity Detach<T>() where T : Component
        {
            var component = _components.FirstOrDefault(c => c is T);
            if (component != null)
            {
                _components.Remove(component);
                component.DetachFromEntity();
            }
            else
            {
                throw new InvalidOperationException($"Component of type {typeof(T)} does not exist in entity {Name}.");
            }

            return this;
        }

        public bool HasComponent<T>() where T : Component => _components.Any(c => c.GetType() == typeof(T));
        public bool HasComponent(Type type) => _components.Any(c => c.GetType() == type);

        public T? GetComponent<T>() where T : Component=> (T?)_components.FirstOrDefault(c => c.GetType() == typeof(T));

        public IReadOnlyList<T> GetComponentsOfType<T>() where T : Component => _components.OfType<T>().ToList();
        public IReadOnlyList<Component> GetComponents() => _components.AsReadOnly();
    }
}
