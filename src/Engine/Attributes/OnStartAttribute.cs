using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OnStartAttribute : Attribute
    {
        public OnStartAttribute()
        {
        }
    }
}
