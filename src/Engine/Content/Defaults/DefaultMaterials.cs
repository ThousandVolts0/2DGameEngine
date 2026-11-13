using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Content.Defaults
{
    public class DefaultMaterials
    {
        public UnlitMaterial Unlit => _unlit.Clone();
        private readonly UnlitMaterial _unlit;
        public DefaultMaterials(UnlitMaterial unlit)
        {
            _unlit = unlit;
        }
    }
}
