using System.Diagnostics.CodeAnalysis;

namespace Engine.Mathematics
{
    public struct Vector2
    {
        public float x { get; set; }
        public float y { get; set; }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.x;
                    case 1: return this.y;
                    default: throw new IndexOutOfRangeException("Invalid Vector2 index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: this.x = value; break;
                    case 1: this.y = value; break;
                    default: throw new IndexOutOfRangeException("Invalid Vector2 index");
                }
            }
        }

        public float Magnitude => (float)Math.Sqrt(x * x + y * y);
        public Vector2 Normalized => this / Magnitude;
        public float Dot(Vector2 other) => x * other.x + y * other.y;

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(b.x * a, b.y * a);
        }

        // Component-wise multiplication
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2(b.x / a, b.y / a);
        }

        // Component-wise division
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static float Dot(Vector2 a, Vector2 b) => a.x * b.x + a.y * b.y;

        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 MinusOne => new Vector2(-1, -1);
        public static Vector2 Infinity => new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);

        public override string ToString() => $"({x}, {y})";
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Vector2 vec && this == vec;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }

}
