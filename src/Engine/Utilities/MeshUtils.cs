namespace Engine.Utilities
{
    /// <summary>
    /// Provides utility methods for generating and manipulating vertice and indicie data for meshes
    /// </summary>
    public static class MeshUtils
    {
        public static float[] CreateCircularVertices(int steps)
        {
            float[] vertices = new float[(steps + 1) * 3];

            // Center
            vertices[0] = 0;
            vertices[1] = 0;
            vertices[2] = 0;

            for (int i = 0; i < steps; i++)
            {
                var theta = i * 2 * MathF.PI / steps;

                float x = MathF.Cos(theta);
                float y = MathF.Sin(theta);

                int index = (i + 1) * 3;
                vertices[index] = x / 2; // Divide by 2 to fit within the unit square
                vertices[index + 1] = y / 2; // Divide by 2 to fit within the unit square
                vertices[index + 2] = 0;
            }

            return vertices;
        }

        public static uint[] CreateCircularIndicies(int steps)
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
