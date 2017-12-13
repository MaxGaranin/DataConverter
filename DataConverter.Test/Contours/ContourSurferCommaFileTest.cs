using NUnit.Framework;
using DataConverter.Points;
using DataConverter.Contours;

namespace DataConverter.Test.Contours
{
    [TestFixture]
    public class ContourSurferCommaFileTest
    {
        private const string FILE_IN1 = @"..\..\data\Contours\SurferBlnComma\Normal1.bln";
        private const string FILE_IN2 = @"..\..\data\Contours\SurferBlnComma\Normal2.bln";
        private const string FILE_OUT = @"..\..\data\Contours\SurferBlnComma\NormalOut.bln";
        
        private const string FILE_NOT_NUMBER = @"..\..\data\Contours\SurferBlnComma\Bad_NotNumber.bln";
        private const string FILE_WRONG_POINT_COUNT = @"..\..\data\Contours\SurferBlnComma\Bad_WrongPointCount.bln";

        [Test]
        public void ReadFile()
        {
            Contour contour = new ContourSurferCommaFile().Read(FILE_IN1);

            Assert.True(contour.Polygons.Count == 2);
            if (contour.Polygons.Count == 2)
            {
                Assert.True(contour.Polygons[0].Points.Count == 6);
                Assert.True(contour.Polygons[1].Points.Count == 2);

                Assert.True(contour.Polygons[0].Points[4].X == 573413.882478);
                Assert.True(contour.Polygons[0].Points[4].Y == 547412.896476);

                Assert.True(contour.Polygons[1].Points[0].X == 575042.238886);
                Assert.True(contour.Polygons[1].Points[0].Y == 548419.51651);
            }

            contour = new ContourSurferCommaFile().Read(FILE_IN2);

            Assert.True(contour.Polygons.Count == 2);
            if (contour.Polygons.Count == 2)
            {
                Assert.True(contour.Polygons[0].Points.Count == 6);
                Assert.True(contour.Polygons[1].Points.Count == 2);

                Assert.True(contour.Polygons[0].Points[5].X == 573828.373394);
                Assert.True(contour.Polygons[0].Points[5].Y == 549159.678352);
                Assert.True(contour.Polygons[0].Points[5].Z == 1.0);

                Assert.True(contour.Polygons[1].Points[1].X == 577055.478954);
                Assert.True(contour.Polygons[1].Points[1].Y == 548212.271052);
                Assert.True(contour.Polygons[1].Points[1].Z == 2.0);
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

            new ContourSurferCommaFile().Write(FILE_OUT, contour);

            contour = new ContourSurferCommaFile().Read(FILE_OUT);
            Assert.True(contour.Polygons.Count == 3);
            if (contour.Polygons.Count == 3)
            {
                Assert.True(contour.Polygons[0].Points.Count == 3);
                Assert.True(contour.Polygons[1].Points.Count == 2);
                Assert.True(contour.Polygons[2].Points.Count == 2);

                Assert.True(contour.Polygons[0].Points[2].X == 3.1);
                Assert.True(contour.Polygons[0].Points[2].Y == 3.2);
                Assert.True(contour.Polygons[0].Points[2].Z == 3.3);

                Assert.True(contour.Polygons[2].Points[1].X == 4);
                Assert.True(contour.Polygons[2].Points[1].Y == 5);
                Assert.True(contour.Polygons[2].Points[1].Z == 6);
            }
        }
    }
}
