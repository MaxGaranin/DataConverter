using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataConverter.Contours
{
    public class ContourProcessor
    {
        public ILogger Logger { get; private set; }

        public ContourProcessor() : this(new DefaultLogger())
        {
        }

        public ContourProcessor(ILogger logger)
        {
            Logger = logger;
        }

        public void Process(IList<string> filesIn, IList<string> filesOut,
            ContourFormat formatIn, ContourFormat formatOut, bool changeFileExtension)
        {
            ContourFileFactory factory = new ContourFileFactory();
            IDataFile<Contour> dataFileIn = factory.GetFile(formatIn);
            IDataFile<Contour> dataFileOut = factory.GetFile(formatOut);
            int successCount = 0;

            for (int i = 0; i < filesIn.Count(); i++)
            {
                try
                {
                    Logger.AddMessage(String.Format("Обрабатывается файл '{0}'. {1} из {2}", 
                        filesIn[i], i+1, filesIn.Count));

                    Contour contour = dataFileIn.Read(filesIn[i]);
                    if (changeFileExtension)
                    {
                        filesOut[i] = Path.ChangeExtension(filesOut[i], dataFileOut.DefaultExtension);
                    }
                    dataFileOut.Write(filesOut[i], contour);

                    successCount++;
                    Logger.AddMessage(String.Format("Файл успешно обработан."));
                }
                catch (Exception e)
                {
                    Logger.AddMessage(String.Format("Ошибка при обработке файла. {0}", e.Message));
                }
            }
            Logger.AddEmptyLine();
            Logger.AddMessage(String.Format("Успешно обработано файлов: {0} из {1}.", successCount, filesIn.Count));
        }

        public void Process(IList<string> filesIn, IList<string> filesOut,
            ContourFormat formatIn, ContourFormat formatOut)
        {
            Process(filesIn, filesOut, formatIn, formatOut, false);
        }
    }
}
