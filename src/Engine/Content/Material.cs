using Engine.Rendering.Shaders;
using System.Drawing;

namespace Engine.Data
{
    public class Material
    {
        public ShaderProgram Program { get; }
        public Color Color { get; }

        public Material(ShaderProgram program) : this(program, Color.White) { }

        public Material(ShaderProgram program, Color color)
        {
            Program = program;
            Color = color;
        }
    }
}
