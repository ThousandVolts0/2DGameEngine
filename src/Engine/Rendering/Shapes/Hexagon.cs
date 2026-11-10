using Engine.Content;
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering
{
    public class Hexagon : IRenderable
    {
        public Mesh Mesh => Mesh.Hexagon;
        public Material? Material { get; }

        public Hexagon(Material? material = null)
        {
            Material = material;
        }
    }
}
