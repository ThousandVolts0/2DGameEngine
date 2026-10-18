using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OnTickAttribute : Attribute
    {
        public OnTickAttribute()
        {
        }
    }
}
