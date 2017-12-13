using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataConverter.Grids2D
{
    public class Grid2DProcessor
    {
        public ILogger Logger { get; private set; }

        public Grid2DProcessor() : this(new DefaultLogger())
        {
        }

        public Grid2DProcessor(ILogger logger)
        {
            Logger = logger;
        }

        public void Process(IList<string> filesIn, IList<string> filesOut,
            Grid2DFormat formatIn, Grid2DFormat formatOut, bool changeFileExtension)
        {
            Grid2DFileFactory factory = new Grid2DFileFactory();
            IDataFile<Grid2D> dataFileIn = factory.GetFile(formatIn);
            IDataFile<Grid2D> dataFileOut = factory.GetFile(formatOut);
            int successCount = 0;

            for (int i = 0; i < filesIn.Count(); i++)
            {
                try
                {
                    Logger.AddMessage(String.Format("Обрабатывается файл '{0}'. {1} из {2}",
                        filesIn[i], i + 1, filesIn.Count));

                    Grid2D grid2D = dataFileIn.Read(filesIn[i]);
                    if (changeFileExtension)
                    {
                        filesOut[i] = Path.ChangeExtension(filesOut[i], dataFileOut.DefaultExtension);
                    }
                    dataFileOut.Write(filesOut[i], grid2D);

                    successCount++;
                    Logger.AddMessage(String.Format("Файл успешно обработан."));
                }
                catch (Exception e)
                {
                    Logger.AddMessage(
                        String.Format("Ошибка при обработке файла. {0}", e.Message));
                }
            }
            Logger.AddEmptyLine();
            Logger.AddMessage(String.Format("Успешно обработано файлов: {0} из {1}.", successCount, filesIn.Count));
        }

        public void Process(IList<string> filesIn, IList<string> filesOut,
            Grid2DFormat formatIn, Grid2DFormat formatOut)
        {
            Process(filesIn, filesOut, formatIn, formatOut, false);
        }

    }
}
