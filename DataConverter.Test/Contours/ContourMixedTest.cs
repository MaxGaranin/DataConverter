using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using DataConverter.Points;
using DataConverter.Contours;

namespace DataConverter.Test.Contours
{
    [TestFixture]
    public class ContourMixedTest
    {
        private const string FILE_ORIG = @"..\..\data\Contours\Mixed\Orig";
        private const string FILE_TEST = @"..\..\data\Contours\Mixed\Test";
        
        [Test]
        public void ReadWriteFile()
        {
            var roxarFile = new ContourRoxarFile();
            Contour contour = roxarFile.Read(FILE_ORIG);
            CheckContour(contour);

            var surferSpaceFile = new ContourSurferSpaceFile();
            surferSpaceFile.Write(FILE_TEST, contour);
            contour = surferSpaceFile.Read(FILE_TEST);
            CheckContour(contour);

            var surferCommaFile = new ContourSurferCommaFile();
            surferCommaFile.Write(FILE_TEST, contour);
            contour = surferCommaFile.Read(FILE_TEST);
            CheckContour(contour);

            roxarFile.Write(FILE_TEST, contour);
            contour = roxarFile.Read(FILE_ORIG);
            CheckContour(contour);
        }

        private static void CheckContour(Contour contour)
        {
            Assert.True(contour.Polygons.Count == 2);
            if (contour.Polygons.Count == 2)
            {
                Assert.True(contour.Polygons[0].Points.Count == 11);
                Assert.True(contour.Polygons[1].Points.Count == 5);

                Assert.True(contour.Polygons[0].Points[7].x == 40047.434875);
                Assert.True(contour.Polygons[0].Points[7].y == 106433.270996);

                Assert.True(contour.Polygons[1].Points[4].x == 38443.472656);
                Assert.True(contour.Polygons[1].Points[4].y == 104740.210938);
            }
        }

    }
}
