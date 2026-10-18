using Engine.Core;
using Engine.Mathematics;

namespace Engine.Components
{
    public sealed class Transform : Component
    {
        // Using ref returns to allow modyfying individual fields of value types directly
        // (e.g. Position.X = 10) without needing to reassign the entire value

        private Vector2 _position = Vector2.Zero;
        public ref Vector2 Position => ref _position;

        private Vector2 _scale = Vector2.One;
        public ref Vector2 Scale => ref _scale;

        private float _rotation = 0f;
        public ref float Rotation => ref _rotation;

        public Transform(Vector2 position, Vector2 scale, float rotation)
        {
            _position = position;
            _scale = scale;
            _rotation = rotation;
        }

        public Transform() { }
    }
}
