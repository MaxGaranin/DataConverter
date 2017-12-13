using System;
using System.IO;
using DataConverter.Helpers;
using DataConverter.Points;

namespace DataConverter.Contours
{
    public class ContourRoxarFile : IDataFile<Contour>
    {
        public string DefExt = "";

        public const double BlockDel = 999.0;
        public const int LinePad = 12;

        /// <summary>
        /// Чтение контура из файла формата RMS ASCII
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

                while ((sLine = sr.ReadLine()) != null)
                {
                    if (sLine.Trim().Length == 0) continue;

                    string[] sTok = sLine.GetTokens(true);

                    PointD p = new PointD();
                    try
                    {
                        p.X = sTok[0].StrToDouble();
                        p.Y = sTok[1].StrToDouble();
                        p.Z = sTok[2].StrToDouble();
                    }
                    catch (Exception e)
                    {
                        throw new FileFormatException("Нечисловое значение координат точки!", row);
                    }

                    if ((p.X == BlockDel) && (p.Y == BlockDel) && (p.Z == BlockDel))
                    {
                        // Новый полигон
                        if (polygon.Points.Count > 0)
                        {
                            contour.Polygons.Add(polygon);
                        }
                        polygon = new Polygon();
                    }
                    else
                    {
                        polygon.Points.Add(p);
                    }

                    row++;
                }
                // Обработка последней записи
                if (polygon.Points.Count > 0)
                {
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
        /// Запись контура в файл формата Roxar ASCII
        /// </summary>
        /// <param name="fileName">Путь к файлу</param> 
        /// <param name="contour">Контур для записи</param>
        public void Write(string fileName, Contour contour)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                foreach (var polygon in contour.Polygons)
                {
                    foreach (var point in polygon.Points)
                    {
                        sw.WriteLine(StringHelper.StrLineRightPad(
                            LinePad, true, point.X, point.Y, point.Z));
                    }
                    sw.WriteLine(StringHelper.StrLineRightPad(
                        LinePad, true, true, BlockDel, BlockDel, BlockDel));
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