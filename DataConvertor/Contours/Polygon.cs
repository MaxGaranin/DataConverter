using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataConverter.Points;

namespace DataConverter.Contours
{
    public class Polygon 
    {
        public List<PointD> Points { get; set; }

        public Polygon()
        {
            Points = new List<PointD>();
        }
    }
}
