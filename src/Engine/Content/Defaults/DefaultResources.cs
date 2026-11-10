using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Content.Defaults
{
    public class DefaultResources
    {
        public DefaultMaterials Materials { get; }
        public DefaultResources(DefaultMaterials materials)
        {
            Materials = materials;
        }
    }
}
