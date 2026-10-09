using Engine.Content;
using Xunit;

namespace Engine.Tests
{
    public class ResourceLoadingTests
    {
        [Fact]
        public void Resources_LoadImageCorrectly()
        {
            var image = Resources.Load<Texture>("test.jpg");
            Assert.NotNull(image);
            Assert.Equal(500, image.Width);
            Assert.Equal(500, image.Height);
        }
    }
}
