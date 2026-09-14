namespace Engine.Rendering
{
    public struct RenderObjectData
    {
        public float[] Vertices { get; set; }
        public uint[] Indices { get; set; }
        public string VertexShader { get; set; }
        public string FragmentShader { get; set; }


        public static RenderObjectData Quad => new RenderObjectData()
        {
            Vertices = new float[]
            {
                 0.5f,  0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.0f
            },
            Indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0
            },
            VertexShader = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * vec4(aPos, 1.0);
                }",
            FragmentShader = @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 0.5, 0.2, 1.0);
                }"
        };
        public static RenderObjectData Triangle => new RenderObjectData()
        {
            Vertices = new float[]
            {
                0.0f,  0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
            },
            Indices = new uint[]
            {
                0, 1, 2
            },
            VertexShader = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * vec4(aPos, 1.0);
                }",
            FragmentShader = @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 0.5, 0.2, 1.0);
                }"
        };
        public static RenderObjectData Circle => new RenderObjectData()
        {
            Vertices = CreateCircularVertices(32),
            Indices = CreateCircularIndicies(32),
            VertexShader = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * vec4(aPos, 1.0);
                }",
            FragmentShader = @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 0.5, 0.2, 1.0);
                }"
        };

        public static RenderObjectData Hexagon => new RenderObjectData()
        {
            Vertices = CreateCircularVertices(6),
            Indices = CreateCircularIndicies(6),
            VertexShader = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * vec4(aPos, 1.0);
                }",
            FragmentShader = @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 0.5, 0.2, 1.0);
                }"
        };


        private static float[] CreateCircularVertices(int steps)
        {
            float[] vertices = new float[(steps+1) * 3];

            // Center
            vertices[0] = 0;
            vertices[1] = 0;
            vertices[2] = 0;

            for (int i = 0; i < steps; i++)
            {
                var theta = i * 2 * MathF.PI / steps;

                float x = MathF.Cos(theta);
                float y = MathF.Sin(theta);

                int index = (i+1) * 3;
                vertices[index] = x / 2; // Divide by 2 to fit within the unit square
                vertices[index + 1] = y / 2; // Divide by 2 to fit within the unit square
                vertices[index + 2] = 0;
            }

            return vertices;
        }

        private static uint[] CreateCircularIndicies(int steps)
        {
            List<uint> indices = new List<uint>();
            for (int i = 0; i < steps; i++)
            {
                indices.Add(0);
                indices.Add((uint)i + 1);
                indices.Add((uint)(i + 1) % (uint)steps + 1);
            }
            return indices.ToArray();
        }
    }
}
