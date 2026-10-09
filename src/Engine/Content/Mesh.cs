namespace Engine.Content
{
    public class Mesh
    {
        public required float[] Vertices { get; init; }
        public required uint[] Indices { get; init; }

        public static Mesh Quad => new Mesh()
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
            }
        };
        public static Mesh Triangle => new Mesh()
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
            }
        };
        public static Mesh Circle => new Mesh()
        {
            Vertices = CreateCircularVertices(32),
            Indices = CreateCircularIndicies(32)
        };

        public static Mesh Hexagon => new Mesh()
        {
            Vertices = CreateCircularVertices(6),
            Indices = CreateCircularIndicies(6)
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
