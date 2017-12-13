using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2D
    {
        public int nY { get; set; }
        public int nX { get; set; }
        public double xStep { get; set; }
        public double yStep { get; set; }

        public double xMin { get; set; }
        public double xMax { get; set; }
        public double yMin { get; set; }
        public double yMax { get; set; }
        public double zMin { get; set; }
        public double zMax { get; set; }
        
        public double Rotation { get; set; }
        public double Blanc { get; set; }

        public double[,] Values { get; set; }
    }
}
