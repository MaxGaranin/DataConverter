using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using DataConverter.Helpers;

namespace DataConverter.Points
{
    [TypeConverter(typeof(EnumTypeConverter))]
    public enum PointFormat
    {
        [Description("XYZ")]
        Xyz,
        [Description("Roxar internal point format")]
        RoxarIpf
    }

}
