using System.Collections.Generic;

namespace DataConverter.Contours
{
    public class Contour
    {
        public IList<Polygon> Polygons { get; set; }

        public Contour()
        {
            Polygons = new List<Polygon>();
        }
    }
}
