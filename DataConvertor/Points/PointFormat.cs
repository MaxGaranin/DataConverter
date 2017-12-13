using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace DataConverter.Points
{
    [TypeConverter(typeof(EnumTypeConverter))]
    public enum PointFormat
    {
        [Description("XYZ")]
        XYZ,
        [Description("Roxar internal point format")]
        RoxarIPF
    }

}
