using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataConverter.Grids2D
{
    [TypeConverter(typeof(EnumTypeConverter))]
    public enum Grid2DFormat
    {
        [Description("Surfer ASCII Grid")]
        SurferText,
        [Description("Surfer Binary Grid")]
        SurferBinary,
        [Description("Roxar ASCII")]
        RoxarText
    }
}
