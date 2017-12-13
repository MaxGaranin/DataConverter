using System;
using System.IO;
using DataConverter.Helpers;

namespace DataConverter.Grids2D
{
    public class Grid2DSurferTextFile : IDataFile<Grid2D>
    {
        public string DefExt = "grd";

        public const string SurferMark = "DSAA";
        public const double SurferNull = 1.79769E+308;

        public const int LinePad = 12;

        /// <summary>
        /// Чтение двумерного грида из файла формата Surfer ASCII
        /// </summary>
        /// <param name="fileName">Путь у файлу</param>
        /// <returns></returns>
        public Grid2D Read(string fileName)
        {
            Grid2D grid = new Grid2D();
            grid.Blanc = SurferNull;
            grid.Rotation = 0.0;

            StreamReader sr = new StreamReader(fileName);
            int row = 1;
            try
            {
                // Читаем заголовок файла
                // 1 строка
                string sLine = sr.ReadLine();
                string[] tokens = sLine.GetTokens(true);
                if (tokens[0] != SurferMark)
                    throw new FileFormatException("Отсутствует ключевое слово DSAA!", row);
                row++;

                // 2 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                grid.NX = Int32.Parse(tokens[0]);
                grid.NY = Int32.Parse(tokens[1]);
                row++;

                // 3 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                try
                {
                    grid.XMin = tokens[0].StrToDouble();
                    grid.XMax = tokens[1].StrToDouble();
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;
                
                // 4 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                try
                {
                    grid.YMin = tokens[0].StrToDouble();
                    grid.YMax = tokens[1].StrToDouble();
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 5 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                try
                {
                    grid.ZMin = tokens[0].StrToDouble();
                    grid.ZMax = tokens[1].StrToDouble();
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                grid.XStep = (grid.XMax - grid.XMin) / (grid.NX - 1);
                grid.YStep = (grid.YMax - grid.YMin) / (grid.NY - 1);
                grid.Values = new double[grid.NY, grid.NX];

                int i = 0;
                int j = 0;
                while ((sLine = sr.ReadLine()) != null)
                {
                    tokens = sLine.GetTokens(true);

                    foreach (string tok in tokens)
                    {
                        try
                        {
                            grid.Values[i, j] = tok.StrToDouble();
                        }
                        catch (Exception e)
                        {
                            throw new FileFormatException("Нечисловое значение координаты точки!", row);
                        }

                        j++;
                        if (j >= grid.NX)
                        {
                            i++;
                            if (i < grid.NY)
                                j = 0;
                        }
                    }
                    row++;
                }

                if ((i != grid.NY) || (j != grid.NX))
                    throw new FileFormatException("Несоответствие заявленного и реального количества ячеек!");
            }
            catch (Exception e)
            {
                throw new FileFormatException(MessageWithRow(e.Message, row));
            }
            finally
            {
                sr.Close();
            }

            return grid;
        }

        /// <summary>
        /// Запись двумерного грида в файл формата Surfer ASCII
        /// </summary>
        /// <param name="fileName">Путь к файлу</param> 
        /// <param name="grid">Грид для записи</param>
        public void Write(string fileName, Grid2D grid)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                sw.WriteLine(SurferMark);
                sw.WriteLine("{0}   {1}", grid.NX, grid.NY);
                sw.WriteLine(StringHelper.StrLineRightPad(LinePad, true, grid.XMin, grid.XMax));
                sw.WriteLine(StringHelper.StrLineRightPad(LinePad, true, grid.YMin, grid.YMax));
                sw.WriteLine(StringHelper.StrLineRightPad(LinePad, true, grid.ZMin, grid.ZMax));

                for (int i = 0; i < grid.NY; i++)
                {
                    for (int j = 0; j < grid.NX; j++)
                    {
                        double val = (grid.Values[i, j] != grid.Blanc) ? grid.Values[i, j] : SurferNull;
                        string s = val.DoubleToStr(StringPadDirection.Right, LinePad);
                        sw.Write(s);
                    }
                    sw.WriteLine();
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

        private static string MessageWithRow(string message, int row)
        {
            return String.Format("Строка: {0}. {1}", row, message);
        }
    }
}