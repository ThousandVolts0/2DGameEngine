using Engine.Mathematics;

namespace Engine.Components
{
    public class Transform : IComponent
    {
        private Vector2 _position = Vector2.Zero;
        public ref Vector2 Position => ref _position;

        private Vector2 _scale = Vector2.One;
        public ref Vector2 Scale => ref _scale;
        public float Rotation { get; set; } = 0;
    }
}
