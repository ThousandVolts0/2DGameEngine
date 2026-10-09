using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Events
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventAttribute : Attribute
    {
        public EventAttribute()
        {
        }
    }
}
