using Engine.Content;
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering
{
    public class Circle : IRenderable
    {
        public Mesh Mesh => Mesh.Circle;
        public Material? Material { get; }

        public Circle(Material? material = null)
        {
            Material = material;
        }
    }
}
