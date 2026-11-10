using Engine.Content;
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering
{
    public class Triangle : IRenderable
    {
        public Mesh Mesh => Mesh.Triangle;
        public Material? Material { get; }

        public Triangle(Material? material = null)
        {
            Material = material;
        }
    }
}
