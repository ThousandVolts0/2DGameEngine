using Silk.NET.OpenGL;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Mathematics
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    default: throw new IndexOutOfRangeException("Invalid Vector2 index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: this.X = value; break;
                    case 1: this.Y = value; break;
                    default: throw new IndexOutOfRangeException("Invalid Vector2 index");
                }
            }
        }

        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);
        public Vector2 Normalized => this / Magnitude;
        public float Dot(Vector2 other) => X * other.X + Y * other.Y;


        // Operator overrides for vector arithmetic
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(b.X * a, b.Y * a);
        }

        // Component-wise multiplication
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2(b.X / a, b.Y / a);
        }

        // Component-wise division
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static float Dot(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;

        // Predefined vectors
        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 MinusOne => new Vector2(-1, -1);
        public static Vector2 Infinity => new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);

        public override string ToString() => $"({X}, {Y})";
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Vector2 vec && this == vec;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

}
