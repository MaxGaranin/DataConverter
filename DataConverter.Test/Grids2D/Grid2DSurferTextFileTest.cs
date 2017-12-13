using NUnit.Framework;
using DataConverter.Grids2D;

namespace DataConverter.Test.Grids2D
{
    [TestFixture]
    public class Grid2DSurferTextFileTest
    {
        private const string FILE_IN = @"..\..\data\Surfaces\SurferText\TopTL.grd";
        private const string FILE_OUT = @"..\..\data\Surfaces\SurferText\TopTLOut.grd";

        private const string FILE_NOT_NUMBER = @"..\..\data\Surfaces\SurferText\Bad_NotNumber.grd";

        [Test]
        public void ReadFile()
        {
            Grid2D grid = new Grid2DSurferTextFile().Read(FILE_IN);

            Assert.True(grid.NX == 819);
            Assert.True(grid.NY == 316);
            Assert.True(grid.XStep == 50);
            Assert.True(grid.YStep == 50);
            Assert.True(grid.XMin == 336533.8125);
            Assert.True(grid.XMax == 377433.8125);
            Assert.True(grid.YMin == 330839.3125);
            Assert.True(grid.YMax == 346589.3125);
            Assert.True(grid.ZMin == 1296.5996);
            Assert.True(grid.ZMax == 1435.6842);

            Assert.True(grid.Values[0, 0] == 1.1);
            Assert.True(grid.Values[0, 7] == 1.79);
            Assert.True(grid.Values[315, 818] == 45.45);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.NY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.NX - 1));
            Assert.True(grid.Values.Length == grid.NX*grid.NY);
        }

        [Test, ExpectedException(typeof (FileFormatException))]
        public void ReadBadFile_NotNumber()
        {
            new Grid2DSurferTextFile().Read(FILE_NOT_NUMBER);
        }

        [Test]
        public void WriteFile()
        {
            Grid2D grid = new Grid2D();
            grid.NX = 3;
            grid.NY = 2;
            grid.XMin = 1.0;
            grid.XMax = 7.0;
            grid.XStep = 3.0;
            grid.YMin = 2.0;
            grid.YMax = 7.0;
            grid.YStep = 5.0;
            grid.ZMin = 0.4;
            grid.ZMax = 123.2;
            grid.Values = new double[2,3]
                              {
                                  {0.4, 0.6, 3.45},
                                  {123.2, 100.2, 45.99}
                              };
            new Grid2DSurferTextFile().Write(FILE_OUT, grid);

            grid = new Grid2DSurferTextFile().Read(FILE_OUT);
            Assert.True(grid.NX == 3.0);
            Assert.True(grid.NY == 2.0);
            Assert.True(grid.XStep == 3.0);
            Assert.True(grid.YStep == 5.0);
            Assert.True(grid.XMin == 1.0);
            Assert.True(grid.XMax == 7.0);
            Assert.True(grid.YMin == 2.0);
            Assert.True(grid.YMax == 7.0);
            Assert.True(grid.ZMin == 0.4);
            Assert.True(grid.ZMax == 123.2);

            Assert.True(grid.Values[0, 0] == 0.4);
            Assert.True(grid.Values[0, 2] == 3.45);
            Assert.True(grid.Values[1, 1] == 100.2);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.NY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.NX - 1));
            Assert.True(grid.Values.Length == grid.NX*grid.NY);
        }
    }
}