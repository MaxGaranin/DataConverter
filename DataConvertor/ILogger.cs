using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataConverter
{
    public interface ILogger
    {
        void AddMessage(string message);
        void AddEmptyLine();
        void Clear();
    }
}
