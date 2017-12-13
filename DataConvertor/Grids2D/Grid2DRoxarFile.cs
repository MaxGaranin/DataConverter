using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using DataConverter.Points;

namespace DataConverter.Grids2D
{
    public class Grid2DRoxarFile : IDataFile<Grid2D>
    {
        public string DEFAULT_EXTENSION = "";

        public const int ROXAR_MARK = -996;
        public const double ROXAR_NULL = 9999900.0;

        public const int LINE_PAD = 12;

        /// <summary>
        /// Чтение двумерного грида из файла формата Roxar ASCII
        /// </summary>
        /// <param name="fileName">Путь у файлу</param>
        /// <returns></returns>
        public Grid2D Read(string fileName)
        {
            Grid2D grid = new Grid2D();
            grid.Blanc = ROXAR_NULL;
            grid.Rotation = 0.0;

            StreamReader sr = new StreamReader(fileName);
            int row = 1;
            try
            {
                // Читаем заголовок файла
                // 1 строка
                string sLine = sr.ReadLine();
                string[] tokens = StringUtils.GetTokens(sLine, true);

                if (Int32.Parse(tokens[0]) != ROXAR_MARK)
                    throw new FileFormatException("Отсутствует ключевое число -996!", row);

                grid.nY = Int32.Parse(tokens[1]);
                try
                {
                    grid.xStep = StringUtils.StrToDouble(tokens[2]);
                    grid.yStep = StringUtils.StrToDouble(tokens[3]);
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 2 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                try
                {
                    grid.xMin = StringUtils.StrToDouble(tokens[0]);
                    grid.xMax = StringUtils.StrToDouble(tokens[1]);
                    grid.yMin = StringUtils.StrToDouble(tokens[2]);
                    grid.yMax = StringUtils.StrToDouble(tokens[3]);
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 3 строка
                sLine = sr.ReadLine();
                tokens = StringUtils.GetTokens(sLine, true);
                grid.nX = Int32.Parse(tokens[0]);
                row++;

                // 4 строка
                sr.ReadLine();
                row++;

                grid.Values = new double[grid.nY, grid.nX];
                
                grid.zMin = ROXAR_NULL;
                grid.zMax = ROXAR_NULL;

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

                        if (grid.Values[i, j] != ROXAR_NULL)
                        {
                            grid.zMin = DefineZmin(grid.zMin, grid.Values[i, j]);
                            grid.zMax = DefineZmax(grid.zMax, grid.Values[i, j]);
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
            catch(Exception e)
            {
                throw new FileFormatException(e.Message, row);
            }
            finally
            {
                sr.Close();
            }

            return grid;
        }

        private static double DefineZmin(double zMin, double val)
        {
            if (zMin == ROXAR_NULL)
                return val;
            
            return Math.Min(zMin, val);
        }

        private static double DefineZmax(double zMax, double val)
        {
            if (zMax == ROXAR_NULL)
                return val;
            
            return Math.Max(zMax, val);
        }

        /// <summary>
        /// Запись двумерного грида в файл формата Roxar ASCII
        /// </summary>
        /// <param name="fileName">Путь к файлу</param> 
        /// <param name="grid">Грид для записи</param>
        public void Write(string fileName, Grid2D grid)
        {
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                sw.WriteLine(StringUtils.SysSepToDec(
                    String.Format("{0}   {1}   {2}   {3}", ROXAR_MARK, grid.nY, grid.xStep, grid.yStep)));
                sw.WriteLine(StringUtils.SysSepToDec(
                    String.Format("{0}   {1}   {2}   {3}", grid.xMin, grid.xMax, grid.yMin, grid.yMax)));
                sw.WriteLine(StringUtils.SysSepToDec(
                    String.Format("{0}   {1}   {2}   {3}", grid.nX, 0, grid.xMin, grid.yMin)));
                sw.WriteLine("0   0   0   0   0   0   0");

                for (int i = 0; i < grid.nY; i++)
                {
                    for (int j = 0; j < grid.nX; j++)
                    {
                        double val = (grid.Values[i, j] != grid.Blanc) ? grid.Values[i, j] : ROXAR_NULL;
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
