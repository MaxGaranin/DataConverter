using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter
{
    public interface IDataFile<T>
    {
        T Read(string fileName);
        void Write(string fileName, T obj);
        string DefaultExtension { get; }
    }
}
