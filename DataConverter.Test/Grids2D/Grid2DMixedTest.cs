using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using DataConverter.Points;
using DataConverter.Grids2D;

namespace DataConverter.Test.Grids2D
{
    [TestFixture]
    public class Grid2DMixedTest
    {
        private const string FILE_ORIG = @"..\..\data\Surfaces\Mixed\Orig";
        private const string FILE_TEST = @"..\..\data\Surfaces\Mixed\Test";
        
        [Test]
        public void ReadWriteFile()
        {
            var roxarFile = new Grid2DRoxarFile();
            Grid2D grid = roxarFile.Read(FILE_ORIG);
            CheckGrid2D(grid);

            var surferTextFile = new Grid2DSurferTextFile();
            surferTextFile.Write(FILE_TEST, grid);
            grid = surferTextFile.Read(FILE_TEST);
            CheckGrid2D(grid);

            var surferBinaryFile = new Grid2DSurferBinaryFile();
            surferBinaryFile.Write(FILE_TEST, grid);
            grid = surferBinaryFile.Read(FILE_TEST);
            CheckGrid2D(grid);

            roxarFile.Write(FILE_TEST, grid);
            grid = roxarFile.Read(FILE_ORIG);
            CheckGrid2D(grid);
        }

        private static void CheckGrid2D(Grid2D grid)
        {
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

            Assert.True(grid.Values[0, 0] == 1301.1);
            Assert.True(grid.Values[0, 7] == 1301.79);
            Assert.True(grid.Values[315, 818] == 1345.45);

            Assert.True(grid.Values.GetUpperBound(0) == (grid.nY - 1));
            Assert.True(grid.Values.GetUpperBound(1) == (grid.nX - 1));
            Assert.True(grid.Values.Length == grid.nX * grid.nY);
        }

    }
}
