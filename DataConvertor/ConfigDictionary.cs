using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using DataConverter.Contours;
using DataConverter.Grids2D;

namespace DataConverter
{
    [DataContract]
    public class ConfigDictionary
    {
        [DataMember] 
        public Dictionary<DataType, DataTypeStuff> TypeToFormats;

        public const string CONFIG_DICT_FILE_NAME = "config_dictionary.xml";

        private static readonly object lockFlag = new object();
        private static ConfigDictionary instance;

        public static ConfigDictionary Instance
        {
            get
            {
                lock (lockFlag)
                {
                    if (instance == null)
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(Path.Combine(
                                    Path.GetDirectoryName(Application.ExecutablePath), CONFIG_DICT_FILE_NAME), FileMode.Open))
                            {
                                XmlDictionaryReader reader =
                                    XmlDictionaryReader.CreateTextReader(fs, Encoding.UTF8,
                                                                         new XmlDictionaryReaderQuotas(), null);
                                DataContractSerializer ser = new DataContractSerializer(typeof (ConfigDictionary));
                                instance = (ConfigDictionary) ser.ReadObject(reader);
                            }
                        }
                        catch (Exception)
                        {
                            //Если не удалось десериализовать, то просто создаем новый экземпляр
                            instance = new ConfigDictionary();
                            instance.SetDefault();
                        }
                    }
                    return instance;
                }
            }
        }

        public void Save()
        {
            XmlTextWriter xw = new XmlTextWriter(Path.Combine(
                                    Path.GetDirectoryName(Application.ExecutablePath), CONFIG_DICT_FILE_NAME), Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xw);
            DataContractSerializer ser = new DataContractSerializer(this.GetType());
            ser.WriteObject(writer, this);
            writer.Close();
            xw.Close();
        }

        public void Reload()
        {
            instance = null;
        }

        public void SetDefault()
        {
            TypeToFormats = new Dictionary<DataType, DataTypeStuff>();
            TypeToFormats.Add(DataType.Contour, 
                new DataTypeStuff(ContourFormat.RoxarText.ToString(), 
                                  ContourFormat.SurferTextSpace.ToString()));
            TypeToFormats.Add(DataType.Grid2D,
                new DataTypeStuff(Grid2DFormat.RoxarText.ToString(),
                                  Grid2DFormat.SurferText.ToString()));
            TypeToFormats.Add(DataType.Point,
                new DataTypeStuff(Grid2DFormat.RoxarText.ToString(),
                                  Grid2DFormat.SurferText.ToString()));
        }
    }
}