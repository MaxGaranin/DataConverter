using System.IO;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2DSurferBinaryFile : IDataFile<Grid2D>
    {
        public string DefExt = "grd";

        public const int HeaderSection = 0x42525344;
        public const int GridSection = 0x44495247;
        public const int DataSection = 0x41544144;

        public const int HeaderSectionSize = 4;
        public const int GridSectionSize = 72;

        public const int CurrentVersion = 1;

        /// <summary>
        /// Запись двумерного грида в файл формата Surfer Binary Grid
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="grid"></param>
        public void Write(string fileName, Grid2D grid)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII);
            try
            {
                bw.Write(HeaderSection);
                bw.Write(HeaderSectionSize);
                bw.Write(CurrentVersion);

                bw.Write(GridSection);
                bw.Write(GridSectionSize);
                bw.Write(grid.NY);
                bw.Write(grid.NX);
                bw.Write(grid.XMin);
                bw.Write(grid.YMin);
                bw.Write(grid.XStep);
                bw.Write(grid.YStep);
                bw.Write(grid.ZMin);
                bw.Write(grid.ZMax);
                bw.Write(grid.Rotation);
                bw.Write(grid.Blanc);

                bw.Write(DataSection);
                bw.Write(grid.NY * grid.NX * sizeof(double));
                for (int i = 0; i < grid.NY; i++)
                {
                    for (int j = 0; j < grid.NX; j++)
                    {
                        bw.Write(grid.Values[i, j]);
                    }
                }

                bw.Flush();
            }
            finally
            {
                bw.Close();
            }
        }

        public string DefaultExtension
        {
            get { return DefExt; }
        }

        /// <summary>
        /// Чтение двумерного грида из файла формата Surfer Binary Grid
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Grid2D Read(string fileName)
        {
            Grid2D grid = new Grid2D();

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs, Encoding.ASCII);
            try
            {
                bool fReadGridSection = false;

                while (br.PeekChar() > -1)
                {
                    int id = br.ReadInt32();
                    switch (id)
                    {
                        case HeaderSection:
                        {
                            ReadHeaderSection(br, grid);
                            break;
                        }

                        case GridSection:
                        {
                            ReadGridSection(br, grid);
                            fReadGridSection = true;
                            break;
                        }

                        case DataSection:
                        {
                            if (!fReadGridSection)
                                throw new FileFormatException(
                                    "Секция GRID должна находиться в файле раньше секции DATA!");
                            ReadDataSection(br, grid);
                            break;
                        }

                        default:
                        {
                            SkipSection(br);
                            break;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new FileFormatException(e.Message);
            }
            finally
            {
                br.Close();
            }

            return grid;
        }

        private static void ReadHeaderSection(BinaryReader br, Grid2D grid)
        {
            int size = br.ReadInt32();
            if (size != HeaderSectionSize)
                throw new FileFormatException("Неправильный размер секции HEADER!");

            int version = br.ReadInt32();
        }

        private static void ReadGridSection(BinaryReader br, Grid2D grid)
        {
            int size = br.ReadInt32();
            if (size != GridSectionSize)
                throw new FileFormatException("Неправильный размер секции GRID!");

            grid.NY = br.ReadInt32();
            grid.NX = br.ReadInt32();
            grid.XMin = br.ReadDouble();
            grid.YMin = br.ReadDouble();
            grid.XStep = br.ReadDouble();
            grid.YStep = br.ReadDouble();
            grid.ZMin = br.ReadDouble();
            grid.ZMax = br.ReadDouble();
            grid.Rotation = br.ReadDouble();
            grid.Blanc = br.ReadDouble();

            grid.XMax = grid.XMin + grid.XStep * (grid.NX - 1);
            grid.YMax = grid.YMin + grid.YStep * (grid.NY - 1);
        }

        private static void ReadDataSection(BinaryReader br, Grid2D grid)
        {
            int size = br.ReadInt32();
            if (size != grid.NY * grid.NX * sizeof(double))
                throw new FileFormatException("Неправильный размер секции DATA!");

            double[,] data = new double[grid.NY, grid.NX];
            for (int i = 0; i < grid.NY; i++)
            {
                for (int j = 0; j < grid.NX; j++)
                {
                    data[i, j] = br.ReadDouble();
                }
            }
            grid.Values = data;
        }

        private static void SkipSection(BinaryReader br)
        {
            int size = br.ReadInt32();
            if (size <= 0)
                throw new FileFormatException("Неправильный размер дополнительной секции!");
            br.BaseStream.Seek(size, SeekOrigin.Current);
        }
    }
}