using Engine.Components;
using Engine.Core;

namespace Engine.Tests.EntityTests
{
    public class ComponentTests
    {
        [Fact]
        public void Entity_Attach_AddsComponent()
        {
            Transform transform = new Transform();
            Entity player = new Entity("Player");

            Assert.Null(player.GetComponent<Transform>());

            player.Attach(transform);

            Assert.NotNull(player.GetComponent<Transform>());
        }

        [Fact]
        public void Entity_Attach_SetsComponentEntity()
        {
            Transform transform = new Transform();
            Assert.False(transform.IsAttachedToEntity);

            Entity player = new Entity("Player")
                .Attach(transform);

            Assert.True(transform.IsAttachedToEntity);
        }

        [Fact]
        public void Entity_Attach_DisallowsComponentAttachedToOtherEntity()
        {
            Transform transform = new Transform();

            Entity player = new Entity("Player")
                .Attach(transform);

            Entity enemy = new Entity("Enemy");

            Assert.Throws<InvalidOperationException>(
                () => enemy.Attach(transform)
            );
        }

        [Fact]
        public void Entity_Attach_DisallowsComponentOfSameType()
        {
            Entity player = new Entity("Player")
                .Attach(new Transform());

            Assert.Throws<InvalidOperationException>(
                () => player.Attach(new Transform())
            );
        }
    }
}
