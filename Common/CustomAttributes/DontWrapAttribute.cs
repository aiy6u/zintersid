using System;

namespace zintersid.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DontWrapAttribute : Attribute
    {
    }
}