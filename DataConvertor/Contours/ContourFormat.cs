using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using DataConverter.Helpers;

namespace DataConverter.Contours
{
    [TypeConverter(typeof(EnumTypeConverter))]
    public enum ContourFormat
    {
        [Description("Surfer Blanking, разделитель пробел")]
        SurferTextSpace,
        [Description("Surfer Blanking, разделитель запятая")]
        SurferTextComma,
        [Description("Roxar ASCII")]
        RoxarText
    }

}
