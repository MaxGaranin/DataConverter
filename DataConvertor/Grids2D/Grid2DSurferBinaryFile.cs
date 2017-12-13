using System.IO;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2DSurferBinaryFile : IDataFile<Grid2D>
    {
        public string DEFAULT_EXTENSION = "grd";

        public const int HEADER_SECTION = 0x42525344;
        public const int GRID_SECTION = 0x44495247;
        public const int DATA_SECTION = 0x41544144;

        public const int HEADER_SECTION_SIZE = 4;
        public const int GRID_SECTION_SIZE = 72;

        public const int CURRENT_VERSION = 1;

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
                bw.Write(HEADER_SECTION);
                bw.Write(HEADER_SECTION_SIZE);
                bw.Write(CURRENT_VERSION);

                bw.Write(GRID_SECTION);
                bw.Write(GRID_SECTION_SIZE);
                bw.Write(grid.nY);
                bw.Write(grid.nX);
                bw.Write(grid.xMin);
                bw.Write(grid.yMin);
                bw.Write(grid.xStep);
                bw.Write(grid.yStep);
                bw.Write(grid.zMin);
                bw.Write(grid.zMax);
                bw.Write(grid.Rotation);
                bw.Write(grid.Blanc);

                bw.Write(DATA_SECTION);
                bw.Write(grid.nY * grid.nX * sizeof(double));
                for (int i = 0; i < grid.nY; i++)
                {
                    for (int j = 0; j < grid.nX; j++)
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
            get { return DEFAULT_EXTENSION; }
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
                        case HEADER_SECTION:
                        {
                            ReadHeaderSection(br, grid);
                            break;
                        }

                        case GRID_SECTION:
                        {
                            ReadGridSection(br, grid);
                            fReadGridSection = true;
                            break;
                        }

                        case DATA_SECTION:
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
            if (size != HEADER_SECTION_SIZE)
                throw new FileFormatException("Неправильный размер секции HEADER!");

            int version = br.ReadInt32();
        }

        private static void ReadGridSection(BinaryReader br, Grid2D grid)
        {
            int size = br.ReadInt32();
            if (size != GRID_SECTION_SIZE)
                throw new FileFormatException("Неправильный размер секции GRID!");

            grid.nY = br.ReadInt32();
            grid.nX = br.ReadInt32();
            grid.xMin = br.ReadDouble();
            grid.yMin = br.ReadDouble();
            grid.xStep = br.ReadDouble();
            grid.yStep = br.ReadDouble();
            grid.zMin = br.ReadDouble();
            grid.zMax = br.ReadDouble();
            grid.Rotation = br.ReadDouble();
            grid.Blanc = br.ReadDouble();

            grid.xMax = grid.xMin + grid.xStep * (grid.nX - 1);
            grid.yMax = grid.yMin + grid.yStep * (grid.nY - 1);
        }

        private static void ReadDataSection(BinaryReader br, Grid2D grid)
        {
            int size = br.ReadInt32();
            if (size != grid.nY * grid.nX * sizeof(double))
                throw new FileFormatException("Неправильный размер секции DATA!");

            double[,] data = new double[grid.nY, grid.nX];
            for (int i = 0; i < grid.nY; i++)
            {
                for (int j = 0; j < grid.nX; j++)
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