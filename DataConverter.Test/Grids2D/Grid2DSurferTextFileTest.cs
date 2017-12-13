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

            Assert.True(grid.nX == 819);
            Assert.True(grid.nY == 316);
            Assert.True(grid.xStep == 50);
            Assert.True(grid.yStep == 50);
            Assert.True(grid.xMin == 336533.8125);
            Assert.True(grid.xMax == 377433.8125);
            Assert.True(grid.yMin == 330839.3125);
            Assert.True(grid.yMax == 346589.3125);
            Assert.True(grid.zMin == 1296.5996);
            Assert.True(grid.zMax == 1435.6842);

            Assert.True(grid.Values[0, 0] == 1.1);
            Assert.True(grid.Values[0, 7] == 1.79);
            Assert.True(grid.Values[315, 818] == 45.45);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.nY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.nX - 1));
            Assert.True(grid.Values.Length == grid.nX*grid.nY);
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
            grid.nX = 3;
            grid.nY = 2;
            grid.xMin = 1.0;
            grid.xMax = 7.0;
            grid.xStep = 3.0;
            grid.yMin = 2.0;
            grid.yMax = 7.0;
            grid.yStep = 5.0;
            grid.zMin = 0.4;
            grid.zMax = 123.2;
            grid.Values = new double[2,3]
                              {
                                  {0.4, 0.6, 3.45},
                                  {123.2, 100.2, 45.99}
                              };
            new Grid2DSurferTextFile().Write(FILE_OUT, grid);

            grid = new Grid2DSurferTextFile().Read(FILE_OUT);
            Assert.True(grid.nX == 3.0);
            Assert.True(grid.nY == 2.0);
            Assert.True(grid.xStep == 3.0);
            Assert.True(grid.yStep == 5.0);
            Assert.True(grid.xMin == 1.0);
            Assert.True(grid.xMax == 7.0);
            Assert.True(grid.yMin == 2.0);
            Assert.True(grid.yMax == 7.0);
            Assert.True(grid.zMin == 0.4);
            Assert.True(grid.zMax == 123.2);

            Assert.True(grid.Values[0, 0] == 0.4);
            Assert.True(grid.Values[0, 2] == 3.45);
            Assert.True(grid.Values[1, 1] == 100.2);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.nY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.nX - 1));
            Assert.True(grid.Values.Length == grid.nX*grid.nY);
        }
    }
}