using Engine.Rendering.Shaders;
using System.Drawing;

namespace Engine.Data
{
    public class Material
    {
        public required ShaderProgram Program { get; init; }
        public Color Color { get; init; } = Color.White;
    }
}
