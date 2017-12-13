using System;
using System.IO;
using DataConverter.Points;

namespace DataConverter.Contours
{
    public class ContourSurferCommaFile : IDataFile<Contour>
    {
        public static readonly char[] DEL_COMMA = {','};

        public string DEFAULT_EXTENSION = "bln";

        /// <summary>
        /// Чтение контура из файла формата Surfer Blanking с разделителем запятая
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

                    string[] sTok = sLine.Split(DEL_COMMA, StringSplitOptions.None);

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
                            p.x = StringUtils.StrToDouble(sTok[0], true);
                            p.y = StringUtils.StrToDouble(sTok[1], true);
                            if (sTok.Length >= 3)
                                p.z = StringUtils.StrToDouble(sTok[2], true);
                        }
                        catch (FormatException)
                        {
                            throw new FileFormatException("Нечисловое значение координаты точки!", row);
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
        /// Запись контура в файл формата Surfer Blanking с разделителем запятая
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
                    sw.WriteLine("{0}, {1}", polygon.Points.Count, k);
                    k++;
                    foreach (var point in polygon.Points)
                    {
                        string s = String.Format("{0}, {1}, {2}",
                            StringUtils.DoubleToStr(point.x), StringUtils.DoubleToStr(point.y),
                            StringUtils.DoubleToStr(point.z));
                        sw.WriteLine(s);
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
            get { return DEFAULT_EXTENSION; }
        }
    }
}