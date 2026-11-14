namespace Conduit.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OnUpdateAttribute : Attribute
    {
        public OnUpdateAttribute()
        {
        }
    }
}
