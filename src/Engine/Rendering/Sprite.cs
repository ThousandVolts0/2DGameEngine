using Engine.Content;
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering
{
    public class Sprite : IRenderable
    {
        public Mesh Mesh => Mesh.Quad;
        public Material Material { get; }

        public Sprite(Material material)
        {
            Material = material;
        }
    }
}
