namespace Conduit.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OnStartAttribute : Attribute
    {
        public OnStartAttribute()
        {
        }
    }
}
