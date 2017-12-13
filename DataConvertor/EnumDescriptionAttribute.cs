using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter
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
