using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2DFileFactory
    {
        public IDataFile<Grid2D> GetFile(Grid2DFormat Grid2DFormat)
        {
            IDataFile<Grid2D> file;
            switch (Grid2DFormat)
            {
                case Grid2DFormat.SurferText:
                    file = new Grid2DSurferTextFile();
                    break;

                case Grid2DFormat.SurferBinary:
                    file = new Grid2DSurferBinaryFile();
                    break;

                case Grid2DFormat.RoxarText:
                    file = new Grid2DRoxarFile();
                    break;

                default:
                    throw new ArgumentException("Неправильно задан формат файла поверхности!");
            }

            return file;
        }
    }
}
