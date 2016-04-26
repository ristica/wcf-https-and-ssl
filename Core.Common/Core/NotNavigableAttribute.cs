using System;

namespace Core.Common.Core
{
    /// <summary>
    /// just a marker class
    /// marks the property as not navigable 
    /// during object graph serach for the
    /// "dirty" properties
    /// so it will not be included in the search
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNavigableAttribute : Attribute
    {
    }
}
