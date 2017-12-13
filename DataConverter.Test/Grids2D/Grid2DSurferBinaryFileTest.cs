using System;
using NUnit.Framework;
using DataConverter.Grids2D;

namespace DataConverter.Test.Grids2D
{
    [TestFixture]
    public class Grid2DSurferBinaryFileTest
    {
        private const string FILE_IN = @"..\..\data\Surfaces\SurferBinary\B-h-10feb.grd";
        private const string FILE_OUT = @"..\..\data\Surfaces\SurferBinary\B-h-10feb_Out.grd";

        [Test]
        public void ReadFile()
        {
            Grid2D grid = new Grid2DSurferBinaryFile().Read(FILE_IN);

            Assert.True(grid.nX == 49);
            Assert.True(grid.nY == 100);
            Assert.True(grid.xMin == 581944.75);
            Assert.True(grid.xMax == 592694.6875);
            Assert.True(grid.yMin == 5996818.5);
            Assert.True(grid.yMax == 6019131);
            Assert.True(Math.Round(grid.zMin, 2) == -782.13);
            Assert.True(Math.Round(grid.zMax, 2) == -488.33);

            Assert.True(Math.Round(grid.Values[1, 1], 2) == -558.63);
            Assert.True(Math.Round(grid.Values[0, 2], 2) == -558.26);
            Assert.True(Math.Round(grid.Values[99, 48], 2) == -603.97);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.nY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.nX - 1));
            Assert.True(grid.Values.Length == grid.nX*grid.nY);
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
            new Grid2DSurferBinaryFile().Write(FILE_OUT, grid);

            grid = new Grid2DSurferBinaryFile().Read(FILE_OUT);
            Assert.True(grid.nX == 3);
            Assert.True(grid.nY == 2);
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