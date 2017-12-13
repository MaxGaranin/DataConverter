using System;

namespace DataConverter.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        public readonly string Description;

        public EnumDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }
}
