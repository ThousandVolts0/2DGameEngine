namespace Conduit.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OnTickAttribute : Attribute
    {
        public OnTickAttribute()
        {
        }
    }
}
