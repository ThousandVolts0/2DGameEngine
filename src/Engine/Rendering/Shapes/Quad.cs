using Engine.Content;
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering.Shapes
{
    public class Quad : IRenderable
    {
        public Mesh Mesh => Mesh.Quad;
        public Material? Material { get; }

        public Quad(Material? material = null)
        {
            Material = material;
        }
    }
}
