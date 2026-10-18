using Engine.Components;
using Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Tests.EntityTests
{
    public class GetComponentTests
    {
        [Fact]
        public void Entity_GetComponent_ReturnsComponent()
        {
            Entity player = new Entity("Player")
                .Attach(new Transform());

            Assert.NotNull(player.GetComponent<Transform>());
        }

        [Fact]
        public void Entity_GetComponentsOfType_ReturnsComponents()
        {
            Entity player = new Entity("Player")
                .Attach(new Transform());

            Assert.True(player.GetComponentsOfType<Transform>().Count > 0);
        }

        [Fact]
        public void Entity_GetComponents_ReturnsAllComponents()
        {
            Entity player = new Entity("Player")
                .Attach(new Transform())
                .Attach(new Camera());

            Assert.Equal(2, player.GetComponents().Count);
        }
    }
}
