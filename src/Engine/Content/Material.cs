using Engine.Rendering.Shaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Data
{
    public class Material
    {
        public required ShaderProgram Program { get; init; }
    }
}
