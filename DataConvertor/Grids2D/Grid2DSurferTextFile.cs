using System;
using System.IO;

namespace DataConverter.Grids2D
{
    public class Grid2DSurferTextFile : IDataFile<Grid2D>
    {
        public string DEFAULT_EXTENSION = "grd";

        public const string SURFER_MARK = "DSAA";
        public const double SURFER_NULL = 1.79769E+308;

        public const int LINE_PAD = 12;

        /// <summary>
        /// Чтение двумерного грида из файла формата Surfer ASCII
        /// </summary>
        /// <param name="fileName">Путь у файлу</param>
        /// <returns></returns>
        public Grid2D Read(string fileName)
        {
            Grid2D grid = new Grid2D();
            grid.Blanc = SURFER_NULL;
            grid.Rotation = 0.0;

            StreamReader sr = new StreamReader(fileName);
            int row = 1;
            try
            {
                // Читаем заголовок файла
                // 1 строка
                string sLine = sr.ReadLine();
                string[] tokens = StringUtils.GetTokens(sLine, true);
                if (tokens[0] != SURFER_MARK)
                    throw new FileFormatException("Отсутствует ключевое слово DSAA!", row);
                row++;

                // 2 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                grid.nX = Int32.Parse(tokens[0]);
                grid.nY = Int32.Parse(tokens[1]);
                row++;

                // 3 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                try
                {
                    grid.xMin = StringUtils.StrToDouble(tokens[0]);
                    grid.xMax = StringUtils.StrToDouble(tokens[1]);
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;
                
                // 4 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                try
                {
                    grid.yMin = StringUtils.StrToDouble(tokens[0]);
                    grid.yMax = StringUtils.StrToDouble(tokens[1]);
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 5 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                try
                {
                    grid.zMin = StringUtils.StrToDouble(tokens[0]);
                    grid.zMax = StringUtils.StrToDouble(tokens[1]);
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                grid.xStep = (grid.xMax - grid.xMin) / (grid.nX - 1);
                grid.yStep = (grid.yMax - grid.yMin) / (grid.nY - 1);
                grid.Values = new double[grid.nY, grid.nX];

                int i = 0;
                int j = 0;
                while ((sLine = sr.ReadLine()) != null)
                {
                    tokens = StringUtils.GetTokens(sLine, true);

                    foreach (string tok in tokens)
                    {
                        try
                        {
                            grid.Values[i, j] = StringUtils.StrToDouble(tok);
                        }
                        catch (Exception e)
                        {
                            throw new FileFormatException("Нечисловое значение координаты точки!", row);
                        }

                        j++;
                        if (j >= grid.nX)
                        {
                            i++;
                            if (i < grid.nY)
                                j = 0;
                        }
                    }
                    row++;
                }

                if ((i != grid.nY) || (j != grid.nX))
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
                sw.WriteLine(SURFER_MARK);
                sw.WriteLine("{0}   {1}", grid.nX, grid.nY);
                sw.WriteLine(StringUtils.StrLineRightPad(LINE_PAD, true, grid.xMin, grid.xMax));
                sw.WriteLine(StringUtils.StrLineRightPad(LINE_PAD, true, grid.yMin, grid.yMax));
                sw.WriteLine(StringUtils.StrLineRightPad(LINE_PAD, true, grid.zMin, grid.zMax));

                for (int i = 0; i < grid.nY; i++)
                {
                    for (int j = 0; j < grid.nX; j++)
                    {
                        double val = (grid.Values[i, j] != grid.Blanc) ? grid.Values[i, j] : SURFER_NULL;
                        string s = StringUtils.DoubleToStr(val, StringPadDirection.Right, LINE_PAD);
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
            get { return DEFAULT_EXTENSION; }
        }

        private static string MessageWithRow(string message, int row)
        {
            return String.Format("Строка: {0}. {1}", row, message);
        }
    }
}