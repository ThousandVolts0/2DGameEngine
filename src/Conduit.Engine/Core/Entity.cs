using Conduit.Engine.Components;

namespace Conduit.Engine.Core
{
    public class Entity
    {
        /// <summary>
        /// The name of this entity
        /// </summary>
        public string Name { get; set; } = "Entity";

        private readonly List<Component> _components = new List<Component>();

        public Entity(string name)
        {
            Name = name;
        }

        public Entity() { }

        /// <summary>
        /// Attaches the specified <see cref="Component"/> to this entity.
        /// Throws if a component of the exact same runtime type already is attached to this entity or if the specified component already is attached to an entity
        /// </summary>
        /// <param name="component">The <see cref="Component"/> to attach</param>
        /// <returns>This <see cref="Entity"/> instance, to support method chaining</returns>
        /// <exception cref="InvalidOperationException">Thrown if another component of same exact runtime type already exists in this entity or if component already is attached to an entity</exception>
        public Entity Attach(Component component)
        {
            Type type = component.GetType();
            if (HasComponent(type))
                throw new InvalidOperationException($"Component of type '{type.Name}' already exists in entity '{Name}'.");

            if (component.IsAttachedToEntity)
                throw new InvalidOperationException("Component is already attached to an existing entity, detach first");

            _components.Add(component);
            component.AttachToEntity(this);
            return this;
        }

        /// <summary>
        /// Detaches the specified <see cref="Component"/> instance from this entity.
        /// Throws if this entity doesn't contain it
        /// </summary>
        /// <param name="component">The <see cref="Component"/> instance to detach from this entity</param>
        /// <returns>This <see cref="Entity"/> instance, to support method chaining</returns>
        /// <exception cref="InvalidOperationException">Thrown if the specified <see cref="Component"/> instance isn't attached to this <see cref="Entity"/></exception>
        public Entity Detach(Component component)
        {
            bool removed = _components.Remove(component);
            if (!removed)
                throw new InvalidOperationException($"Component of type '{component.GetType().Name}' does not exist in entity '{Name}'.");

            component.DetachFromEntity();

            return this;
        }

        /// <summary>
        /// Detaches the <see cref="Component"/> attached to this entity whose type exactly matches <typeparamref name="T"/> or throws if none found
        /// </summary>
        /// <typeparam name="T">The <see cref="Component"/> type to detach</typeparam>
        /// <returns>This <see cref="Entity"/> instance, to support method chaining</returns>
        /// <exception cref="InvalidOperationException">Thrown when no component of type <typeparamref name="T"/> is attached to this entity</exception>
        public Entity Detach<T>() where T : Component
        {
            var component = GetComponent<T>();
            
            if (component == null)
                throw new InvalidOperationException($"Cannot detach component of type '{typeof(T)}' because it does not exist in entity '{Name}'");

            _components.Remove(component);
            component.DetachFromEntity();

            return this;
        }

        /// <summary>
        /// Determines whether this entity has a <see cref="Component"/> whose runtime type exactly matches <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Component"/> type to check for</typeparam>
        /// <returns>A <see langword="bool"/> representing whether this entity has a <see cref="Component"/> of exact type <typeparamref name="T"/></returns>
        public bool HasComponent<T>() where T : Component => _components.Any(c => c.GetType() == typeof(T));

        /// <summary>
        /// Determines whether this entity has a <see cref="Component"/> whose runtime type exactly matches <paramref name="type"/>
        /// </summary>
        /// <param name="type">The <see cref="Component"/> type to check for</param>
        /// <returns>A <see langword="bool"/> representing whether this entity has a <see cref="Component"/> whose runtime type exactly matches <paramref name="type"/></returns>
        public bool HasComponent(Type type) => _components.Any(c => c.GetType() == type);

        /// <summary>
        /// Gets the <see cref="Component"/> whose runtime type exactly matches <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="Component"/> type to search for</typeparam>
        /// <returns>The <see cref="Component"/> of type <typeparamref name="T"/> or <see langword="null"></see> if none exists</returns>
        public T? GetComponent<T>() where T : Component => (T?)_components.FirstOrDefault(c => c.GetType() == typeof(T));

        /// <summary>
        /// Gets a new <see cref="IReadOnlyList{T}"/> containing all components attached to this entity whose runtime type exactly matches <typeparamref name="T"/>
        /// </summary> 
        /// <typeparam name="T">The component type to search for</typeparam>
        /// <returns>A new <see cref="IReadOnlyList{T}"/> of components exactly matching type <typeparamref name="T"/> or an empty list if none are found</returns>
        public IReadOnlyList<T> GetComponentsOfType<T>() where T : Component => _components.Where(c => c.GetType() == typeof(T)).Cast<T>().ToList();

        /// <summary>
        /// Gets a read-only list of all components attached to this entity
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{Component}"/> containing all attached components</returns>
        public IReadOnlyList<Component> GetComponents() => _components.AsReadOnly();
    }
}
