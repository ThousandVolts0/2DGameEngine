using Engine.Components;

namespace Engine.Core
{
    public class Element
    {
        public string Name { get; set; } = "Element";

        private List<IComponent> _components = new List<IComponent>();

        public Element(string name = "Element")
        {
            Name = name;
        }

        public Element(List<IComponent> components, string name = "Element")
        {
            Name = name;
            _components = components;
        }

        public Element(string name, params IComponent[] components)
        {
            Name = name;
            _components = components.ToList();
        }

        public Element(params IComponent[] components)
        {
            _components = components.ToList();
        }

        public Element() { }

        public Element Attach(IComponent component, Action<IComponent>? configure = null)
        {
            Type type = component.GetType();
            if (HasComponent(type))
                throw new InvalidOperationException($"Component of type {type.Name} already exists in element {Name}.");

            configure?.Invoke(component);

            _components.Add(component);
            return this;
        }

        public Element Attach<T>(Action<T>? configure = null) where T : IComponent, new()
        {
           if (HasComponent<T>())
                throw new InvalidOperationException($"Component of type {typeof(T).Name} already exists in element {Name}.");

            T component = new T();
            configure?.Invoke(component);

            _components.Add(component);
            return this;
        }

        public bool HasComponent<T>() where T : IComponent => _components.Any(c => c.GetType() == typeof(T));
        public bool HasComponent(Type type)  => _components.Any(c => c.GetType() == type);

        public Element Detach(IComponent component)
        {
            bool removed = _components.Remove(component);
            if (!removed)
                throw new InvalidOperationException($"Component of type {component.GetType().Name} does not exist in element {Name}.");

            return this;
        }
        public Element Detach<T>() where T : IComponent
        {
            var component = _components.FirstOrDefault(c => c is T);
            if (component != null)
                _components.Remove(component);

            return this;
        }

        public T? GetComponent<T>() where T : class, IComponent=> (T?)_components.FirstOrDefault(c => c.GetType() == typeof(T));
    }
}
