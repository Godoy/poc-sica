using System;

namespace Sica.Assets.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SwaggerSubtypeOfAttribute : Attribute
    {
        public Type Parent { get; }

        public string Name { get; }

        public SwaggerSubtypeOfAttribute(string name, Type parent)
        {
            this.Name = name;
            this.Parent = parent;
        }
    }
}
