using Engine.Utilities;

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
            Vertices = MeshUtils.CreateCircularVertices(32),
            Indices = MeshUtils.CreateCircularIndicies(32)
        };
        public static Mesh Hexagon => new Mesh()
        {
            Vertices = MeshUtils.CreateCircularVertices(6),
            Indices = MeshUtils.CreateCircularIndicies(6)
        };
    }
}
