using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataConverter
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum p)
        {
            return ((DescriptionAttribute)p.GetType().GetField(p.ToString()).
                GetCustomAttributes(typeof(DescriptionAttribute), true)[0]).Description;
        }

        public static T GetEnumByName<T>(string name)
        {
            return (T) Enum.Parse(typeof(T), name);
        }
    }

}
