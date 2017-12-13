using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataConverter.Contours;
using DataConverter.Grids2D;

namespace DataConverter
{
    /// <summary>
    /// Сохраняемые настройки программы
    /// </summary>
    public class Config
    {
        public const string Version = "1.0.0.3";

        public readonly string AboutProgram = string.Format("DataConverter {0} \n\n" +
                                                             "© ОАО \"Гипровостокнефть\" 2010.\n\n" +
                                                             "Разработчик: Гаранин М.С.\n\n", Version);

        public DataType DataType { get; set; }
        public bool IsSubfolder { get; set; }
        public string OutSubfolder { get; set; }
        public string OutSelectedFolder { get; set; }
        public string LastPath { get; set; }

        public const string ConfigFileName = "config.xml";

        private static readonly object LockFlag = new object();
        private static Config _instance;

        private Config()
        {
        }

        [XmlIgnore]
        public static Config Instance
        {
            get
            {
                lock (LockFlag)
                {
                    if (_instance == null)
                    {
                        try
                        {
                            //Пытаемся загрузить файл с диска и десериализовать его
                            using (FileStream fs =
                                new FileStream(Path.Combine(
                                    Path.GetDirectoryName(Application.ExecutablePath), ConfigFileName), FileMode.Open)
                                )
                            {
                                XmlSerializer xs = new XmlSerializer(typeof (Config));
                                _instance = (Config) xs.Deserialize(fs);
                            }
                        }
                        catch (Exception)
                        {
                            //Если не удалось десериализовать, то просто создаем новый экземпляр
                            _instance = new Config();
                            _instance.SetDefault();
                        }
                    }
                    return _instance;
                }
            }
        }

        public void Save()
        {
            using (FileStream fs =
                new FileStream(Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath), ConfigFileName), FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof (Config));
                xs.Serialize(fs, _instance);
            }
        }

        public static void Reload()
        {
            _instance = null;
        }

        public void SetDefault()
        {
            DataType = DataType.Contour;
            IsSubfolder = true;
            OutSubfolder = "Out";
            OutSelectedFolder = String.Empty;
        }
    }
}