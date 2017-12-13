using System;
using System.IO;
using DataConverter.Helpers;
using DataConverter.Points;

namespace DataConverter.Contours
{
    public class ContourSurferSpaceFile : IDataFile<Contour>
    {
        public string DefExt = "bln";

        public const int LinePad = 12;

        /// <summary>
        /// Чтение контура из файла формата Surfer Blanking с разделителем пробел
        /// </summary>
        /// <param name="fileName">Путь у файлу</param>
        /// <returns></returns>
        public Contour Read(string fileName)
        {
            Contour contour = new Contour();
            StreamReader sr = new StreamReader(fileName);
            try
            {
                Polygon polygon = new Polygon();
                string sLine;
                int row = 1;
                int nPoints = 0;

                while ((sLine = sr.ReadLine()) != null)
                {
                    if (sLine.Trim().Length == 0) continue;

                    string[] sTok = StringHelper.GetTokens(sLine, true);

                    if (nPoints == 0)
                    {
                        // Новый полигон
                        if (polygon.Points.Count > 0)
                        {
                            contour.Polygons.Add(polygon);
                        }

                        if (!int.TryParse(sTok[0], out nPoints))
                            throw new FileFormatException(
                                "Неправильно задано количество точек в полигоне!", row);

                        polygon = new Polygon();
                    }
                    else
                    {
                        // Считываем точки
                        if (sTok.Length == 1)
                            throw new FileFormatException(
                                "В строке должно быть как минимум два значения, координаты x и y!", row);

                        PointD p = new PointD();
                        try
                        {
                            p.X = StringHelper.StrToDouble(sTok[0]);
                            p.Y = StringHelper.StrToDouble(sTok[1]);

                            if (sTok.Length >= 3)
                                p.Z = StringHelper.StrToDouble(sTok[2]);
                        }
                        catch (Exception e)
                        {
                            throw new FileFormatException("Нечисловое значение координат точки!", row);
                        }

                        polygon.Points.Add(p);
                        nPoints--;
                    }

                    row++;
                }
                // Обработка последней записи
                if (polygon.Points.Count > 0)
                {
                    if (nPoints != 0)
                        throw new FileFormatException("Неправильно задано количество точек в полигоне!", row);

                    contour.Polygons.Add(polygon);
                }
            }
            catch (IOException e)
            {
                throw new FileFormatException(e.Message);
            }
            finally
            {
                sr.Close();
            }

            return contour;
        }

        /// <summary>
        /// Запись контура в файл формата Surfer Blanking с разделителем пробел
        /// </summary>
        /// <param name="fileName">Путь к файлу</param> 
        /// <param name="contour">Контур для записи</param>
        public void Write(string fileName, Contour contour)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                int k = 1; // Счетчик полигонов
                foreach (var polygon in contour.Polygons)
                {
                    if (polygon.Points.Count == 0) continue;
                    sw.WriteLine("{0}   {1}", polygon.Points.Count, k);
                    k++;
                    foreach (var point in polygon.Points)
                    {
                        sw.WriteLine(StringHelper.StrLineRightPad(
                            LinePad, true, true, point.X, point.Y, point.Z));
                    }
                }
            }
            finally
            {
                sw.Close();
            }
        }

        public string DefaultExtension
        {
            get { return DefExt; }
        }
    }
}