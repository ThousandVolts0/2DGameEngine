using Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Components
{
    public abstract class Component
    {
        private Entity? _entity = null;
        protected Entity Entity => _entity 
            ?? throw new InvalidOperationException("Component hasn't been attached to entity yet");

        internal bool IsAttachedToEntity => _entity != null;

        internal void AttachToEntity(Entity entity)
        {
            _entity = entity;
        }

        internal void DetachFromEntity()
        {
            _entity = null;
        }
    }
}
