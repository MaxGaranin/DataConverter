using System;
using System.IO;
using DataConverter.Helpers;

namespace DataConverter.Grids2D
{
    public class Grid2DRoxarFile : IDataFile<Grid2D>
    {
        public string DefExt = "";

        public const int RoxarMark = -996;
        public const double RoxarNull = 9999900.0;
        public const int LinePad = 12;

        /// <summary>
        /// Чтение двумерного грида из файла формата Roxar ASCII
        /// </summary>
        /// <param name="fileName">Путь у файлу</param>
        /// <returns></returns>
        public Grid2D Read(string fileName)
        {
            Grid2D grid = new Grid2D();
            grid.Blanc = RoxarNull;
            grid.Rotation = 0.0;

            StreamReader sr = new StreamReader(fileName);
            int row = 1;
            try
            {
                // Читаем заголовок файла
                // 1 строка
                string sLine = sr.ReadLine();
                string[] tokens = sLine.GetTokens(true);

                if (Int32.Parse(tokens[0]) != RoxarMark)
                    throw new FileFormatException("Отсутствует ключевое число -996!", row);

                grid.NY = Int32.Parse(tokens[1]);
                try
                {
                    grid.XStep = tokens[2].StrToDouble();
                    grid.YStep = tokens[3].StrToDouble();
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 2 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                try
                {
                    grid.XMin = tokens[0].StrToDouble();
                    grid.XMax = tokens[1].StrToDouble();
                    grid.YMin = tokens[2].StrToDouble();
                    grid.YMax = tokens[3].StrToDouble();
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Нечисловое значение координаты точки!", row);
                }
                row++;

                // 3 строка
                sLine = sr.ReadLine();
                tokens = sLine.GetTokens(true);
                grid.NX = Int32.Parse(tokens[0]);
                row++;

                // 4 строка
                sr.ReadLine();
                row++;

                grid.Values = new double[grid.NY, grid.NX];

                grid.ZMin = RoxarNull;
                grid.ZMax = RoxarNull;

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

                        if (grid.Values[i, j] != RoxarNull)
                        {
                            grid.ZMin = DefineZmin(grid.ZMin, grid.Values[i, j]);
                            grid.ZMax = DefineZmax(grid.ZMax, grid.Values[i, j]);
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
            if (zMin == RoxarNull)
                return val;

            return Math.Min(zMin, val);
        }

        private static double DefineZmax(double zMax, double val)
        {
            if (zMax == RoxarNull)
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
                sw.WriteLine(String.Format("{0}   {1}   {2}   {3}", RoxarMark, grid.NY, grid.XStep, grid.YStep)
                    .SysSepToDec());
                sw.WriteLine(String.Format("{0}   {1}   {2}   {3}", grid.XMin, grid.XMax, grid.YMin, grid.YMax)
                    .SysSepToDec());
                sw.WriteLine(String.Format("{0}   {1}   {2}   {3}", grid.NX, 0, grid.XMin, grid.YMin).SysSepToDec());
                sw.WriteLine("0   0   0   0   0   0   0");

                for (int i = 0; i < grid.NY; i++)
                {
                    for (int j = 0; j < grid.NX; j++)
                    {
                        double val = (grid.Values[i, j] != grid.Blanc) ? grid.Values[i, j] : RoxarNull;
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
    }
}