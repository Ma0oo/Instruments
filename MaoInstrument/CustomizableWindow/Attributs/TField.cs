using System;

namespace Plugins.Customizable.Attributs
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple =  false, Inherited = true)]
    public class TField : Attribute
    {
        
    }
}