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

            Assert.True(grid.NX == 49);
            Assert.True(grid.NY == 100);
            Assert.True(grid.XMin == 581944.75);
            Assert.True(grid.XMax == 592694.6875);
            Assert.True(grid.YMin == 5996818.5);
            Assert.True(grid.YMax == 6019131);
            Assert.True(Math.Round(grid.ZMin, 2) == -782.13);
            Assert.True(Math.Round(grid.ZMax, 2) == -488.33);

            Assert.True(Math.Round(grid.Values[1, 1], 2) == -558.63);
            Assert.True(Math.Round(grid.Values[0, 2], 2) == -558.26);
            Assert.True(Math.Round(grid.Values[99, 48], 2) == -603.97);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.NY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.NX - 1));
            Assert.True(grid.Values.Length == grid.NX*grid.NY);
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
            new Grid2DSurferBinaryFile().Write(FILE_OUT, grid);

            grid = new Grid2DSurferBinaryFile().Read(FILE_OUT);
            Assert.True(grid.NX == 3);
            Assert.True(grid.NY == 2);
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