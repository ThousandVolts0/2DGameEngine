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

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        public static Vector2 Dot(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);

        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 MinusOne => new Vector2(-1, -1);
        public static Vector2 Infinity => new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);

        public override string ToString() => $"({x}, {y})";
    }

}
