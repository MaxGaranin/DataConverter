using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataConverter.Helpers;

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
