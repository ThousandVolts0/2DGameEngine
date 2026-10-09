using Engine.Mathematics;
using Xunit;

namespace Engine.Tests
{
    public class Vector2Tests
    {
        [Fact]
        public void Constructor_SetsXY()
        {
            Vector2 vec = new Vector2(1, 2);

            Assert.Equal(1, vec.X);
            Assert.Equal(2, vec.Y);
        }

        [Fact]
        public void Addition_Works()
        {
            Vector2 vec1 = new Vector2(1, 2);
            Vector2 vec2 = new Vector2(2, 3);

            Assert.Equal(new Vector2(3,5), vec1 + vec2);
        }

        [Fact]
        public void Subtraction_Works()
        {
            Vector2 vec1 = new Vector2(2, 3);
            Vector2 vec2 = new Vector2(1, 2);

            Assert.Equal(new Vector2(1, 1), vec1 - vec2);
        }

        [Fact]
        public void ScalarMultiplication_Works()
        {
            Vector2 vec = new Vector2(1, 2);
            float value = 2;

            Assert.Equal(new Vector2(2, 4), vec * value);
        }

        [Fact]
        public void ComponentMultiplication_Works()
        {
            Vector2 vec1 = new Vector2(1, 2);
            Vector2 vec2 = new Vector2(2, 3);

            Assert.Equal(new Vector2(2, 6), vec1 * vec2);
        }

        [Fact]
        public void ScalarDivision_Works()
        {
            Vector2 vec = new Vector2(1, 2);
            float value = 2;

            Assert.Equal(new Vector2(0.5f, 1), vec / value);
        }

        [Fact]
        public void ComponentDivision_Works()
        {
            Vector2 vec1 = new Vector2(2, 3);
            Vector2 vec2 = new Vector2(1, 2);

            Assert.Equal(new Vector2(2, 1.5f), vec1 / vec2);
        }

        [Fact]
        public void Magnitude_Works()
        {
            Vector2 vec = new Vector2(3, 4);

            Assert.Equal(5, vec.Magnitude);
        }

        [Fact]
        public void Normalized_Works()
        {
            Vector2 vec = new Vector2(3, 4);

            Assert.Equal(new Vector2(0.6f, 0.8f), vec.Normalized);
        }

        [Fact]
        public void Dot_Works()
        {
            Vector2 vec1 = new Vector2(1, 2);
            Vector2 vec2 = new Vector2(2, 3);

            Assert.Equal(8, vec1.Dot(vec2));
        }
    }
}
