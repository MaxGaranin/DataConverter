using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter.Contours
{
    public class ContourFileFactory
    {
        public IDataFile<Contour> GetFile(ContourFormat contourFormat)
        {
            IDataFile<Contour> file;
            switch (contourFormat)
            {
                case ContourFormat.SurferTextSpace:
                    file = new ContourSurferSpaceFile();
                    break;

                case ContourFormat.SurferTextComma:
                    file = new ContourSurferCommaFile();
                    break;

                case ContourFormat.RoxarText:
                    file = new ContourRoxarFile();
                    break;

                default:
                    throw new ArgumentException("Неправильно задан формат файла контура!");
            }

            return file;
        }
    }
}
