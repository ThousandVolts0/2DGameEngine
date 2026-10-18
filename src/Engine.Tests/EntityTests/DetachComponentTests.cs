using Engine.Components;
using Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Tests.EntityTests
{
    public class DetachComponentTests
    {
        [Fact]
        public void Entity_Detach_RemovesComponent()
        {
            Transform transform = new Transform();
            Entity player = new Entity("Player")
                .Attach(transform);

            Assert.NotNull(player.GetComponent<Transform>());

            player.Detach(transform);

            Assert.Null(player.GetComponent<Transform>());
        }

        [Fact]
        public void Entity_Detach_ThrowsIfComponentDoesntExist()
        {
            Entity player = new Entity("Player");

            Assert.Throws<InvalidOperationException>(
                () => player.Detach<Transform>()
            );
        }

        [Fact]
        public void Entity_Detach_UnsetsComponentEntity()
        {
            Transform transform = new Transform();
            Entity player = new Entity("Player")
                .Attach(transform);

            Assert.True(transform.IsAttachedToEntity);

            player.Detach(transform);

            Assert.False(transform.IsAttachedToEntity);
        }
    }
}
