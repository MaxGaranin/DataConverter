using NUnit.Framework;
using DataConverter.Points;
using DataConverter.Contours;

namespace DataConverter.Test.Contours
{
    [TestFixture]
    public class ContourSurferSpaceFileTest
    {
        private const string FILE_IN1 = @"..\..\data\Contours\SurferBlnSpace\Normal1.bln";
        private const string FILE_IN2 = @"..\..\data\Contours\SurferBlnSpace\Normal2.bln";
        private const string FILE_OUT = @"..\..\data\Contours\SurferBlnSpace\NormalOut.bln";
        
        private const string FILE_NOT_NUMBER = @"..\..\data\Contours\SurferBlnSpace\Bad_NotNumber.bln";
        private const string FILE_WRONG_POINT_COUNT = @"..\..\data\Contours\SurferBlnSpace\Bad_WrongPointsCount.bln";

        [Test]
        public void ReadFile()
        {
            Contour contour = new ContourSurferSpaceFile().Read(FILE_IN1);

            Assert.True(contour.Polygons.Count == 2);
            if (contour.Polygons.Count == 2)
            {
                Assert.True(contour.Polygons[0].Points.Count == 11);
                Assert.True(contour.Polygons[1].Points.Count == 5);

                Assert.True(contour.Polygons[0].Points[7].x == 40047.434875);
                Assert.True(contour.Polygons[0].Points[7].y == 106433.270996);
                Assert.True(contour.Polygons[0].Points[7].z == 1.0);

                Assert.True(contour.Polygons[1].Points[4].x == 38443.472656);
                Assert.True(contour.Polygons[1].Points[4].y == 104740.210938);
                Assert.True(contour.Polygons[1].Points[4].z == 2.0);
            }

            contour = new ContourSurferSpaceFile().Read(FILE_IN2);

            Assert.True(contour.Polygons.Count == 1);
            if (contour.Polygons.Count == 1)
            {
                Assert.True(contour.Polygons[0].Points.Count == 18);

                Assert.True(contour.Polygons[0].Points[0].x == 21831.3);
            }

        }

        [Test, ExpectedException(typeof(FileFormatException))]
        public void ReadBadFile_NotNumber()
        {
            new ContourSurferSpaceFile().Read(FILE_NOT_NUMBER);
        }

        [Test, ExpectedException(typeof(FileFormatException))]
        public void ReadBadFile_WrongPointCount()
        {
            new ContourSurferSpaceFile().Read(FILE_WRONG_POINT_COUNT);
        }

        [Test]
        public void WriteFile()
        {
            Contour contour = new Contour();
            
            Polygon polygon = new Polygon();
            polygon.Points.Add(new PointD(1.1, 1.2, 1.3));
            polygon.Points.Add(new PointD(2.1, 2.2, 2.3));
            polygon.Points.Add(new PointD(3.1, 3.2, 3.3));
            contour.Polygons.Add(polygon);

            polygon = new Polygon();
            polygon.Points.Add(new PointD(10, 20, 0));
            polygon.Points.Add(new PointD(20, 10, 0));
            contour.Polygons.Add(polygon);

            polygon = new Polygon();
            polygon.Points.Add(new PointD(1, 2, 3));
            polygon.Points.Add(new PointD(4, 5, 6));
            contour.Polygons.Add(polygon);

            new ContourSurferSpaceFile().Write(FILE_OUT, contour);

            contour = new ContourSurferSpaceFile().Read(FILE_OUT);
            Assert.True(contour.Polygons.Count == 3);
            if (contour.Polygons.Count == 3)
            {
                Assert.True(contour.Polygons[0].Points.Count == 3);
                Assert.True(contour.Polygons[1].Points.Count == 2);
                Assert.True(contour.Polygons[2].Points.Count == 2);

                Assert.True(contour.Polygons[0].Points[2].x == 3.1);
                Assert.True(contour.Polygons[0].Points[2].y == 3.2);
                Assert.True(contour.Polygons[0].Points[2].z == 3.3);

                Assert.True(contour.Polygons[2].Points[1].x == 4);
                Assert.True(contour.Polygons[2].Points[1].y == 5);
                Assert.True(contour.Polygons[2].Points[1].z == 6);
            }
        }
    }
}
