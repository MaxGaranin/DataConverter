using System;
using System.IO;
using DataConverter.Points;

namespace DataConverter.Contours
{
    public class ContourRoxarFile : IDataFile<Contour>
    {
        public string DEFAULT_EXTENSION = "";

        public const double BLOCK_DEL = 999.0;
        public const int LINE_PAD = 12;

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

                    string[] sTok = StringUtils.GetTokens(sLine, true);

                    PointD p = new PointD();
                    try
                    {
                        p.x = StringUtils.StrToDouble(sTok[0]);
                        p.y = StringUtils.StrToDouble(sTok[1]);
                        p.z = StringUtils.StrToDouble(sTok[2]);
                    }
                    catch (Exception e)
                    {
                        throw new FileFormatException("Нечисловое значение координат точки!", row);
                    }

                    if ((p.x == BLOCK_DEL) && (p.y == BLOCK_DEL) && (p.z == BLOCK_DEL))
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
                        sw.WriteLine(StringUtils.StrLineRightPad(
                            LINE_PAD, true, point.x, point.y, point.z));
                    }
                    sw.WriteLine(StringUtils.StrLineRightPad(
                        LINE_PAD, true, true, BLOCK_DEL, BLOCK_DEL, BLOCK_DEL));
                }
            }
            finally
            {
                sw.Close();
            }
        }

        public string DefaultExtension
        {
            get { return DEFAULT_EXTENSION; }
        }
    }
}