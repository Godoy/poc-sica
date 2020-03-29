using System;

namespace Sica.Assets.Shared.Attributes
{
    public class RequestTypeAttribute : Attribute
    {
        public Type Type { get; set; }

        public RequestTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
