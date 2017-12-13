using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter
{
    public class DataTypeStuff
    {
        public string FormatIn { get; set; }
        public string FormatOut { get; set; }

        public DataTypeStuff()
        {
        }

        public DataTypeStuff(string formatIn, string formatOut)
        {
            FormatIn = formatIn;
            FormatOut = formatOut;
        }
    }
}
