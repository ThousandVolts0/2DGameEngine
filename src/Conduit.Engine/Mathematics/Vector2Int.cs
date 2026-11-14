using Silk.NET.Maths;
using System.Diagnostics.CodeAnalysis;

namespace Conduit.Engine.Mathematics
{
    public struct Vector2Int
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int this[int index]
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

        // Operator overrides for vector arithmetic
        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2Int operator *(Vector2Int a, int b)
        {
            return new Vector2Int(a.X * b, a.Y * b);
        }

        public static Vector2Int operator *(int a, Vector2Int b)
        {
            return new Vector2Int(b.X * a, b.Y * a);
        }

        // Component-wise multiplication
        public static Vector2Int operator *(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2Int operator /(Vector2Int a, int b)
        {
            return new Vector2Int(a.X / b, a.Y / b);
        }

        // Component-wise division
        public static Vector2Int operator /(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X / b.X, a.Y / b.Y);
        }

        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        // Predefined vectors
        public static Vector2Int Zero => new Vector2Int(0, 0);
        public static Vector2Int One => new Vector2Int(1, 1);
        public static Vector2Int MinusOne => new Vector2Int(-1, -1);
        public static Vector2Int MaxValue => new Vector2Int(int.MaxValue, int.MaxValue);

        public static Vector2Int Up => new Vector2Int(0, 1);
        public static Vector2Int Down => new Vector2Int(0, -1);
        public static Vector2Int Right => new Vector2Int(1, 0);
        public static Vector2Int Left => new Vector2Int(-1, 0);

        public static explicit operator Vector2D<int>(Vector2Int v) => new Vector2D<int>(v.X, v.Y);
        public static explicit operator Vector2Int(Vector2D<int> v) => new Vector2Int(v.X, v.Y);

        public override string ToString() => $"({X}, {Y})";
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Vector2Int vec && this == vec;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

}
