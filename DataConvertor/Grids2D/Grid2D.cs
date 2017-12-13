using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2D
    {
        public int NY { get; set; }
        public int NX { get; set; }
        public double XStep { get; set; }
        public double YStep { get; set; }

        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double ZMin { get; set; }
        public double ZMax { get; set; }
        
        public double Rotation { get; set; }
        public double Blanc { get; set; }

        public double[,] Values { get; set; }
    }
}
