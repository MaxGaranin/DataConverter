using System;
using System.Runtime.Serialization;

namespace DataConverter
{
    public class FileFormatException : ApplicationException
    {
        protected FileFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FileFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FileFormatException(string message) : base(message)
        {
        }

        public FileFormatException(string message, int row)
            : base(String.Format("Строка: {0}. {1}", row, message))
        {
        }

        public FileFormatException()
        {
        }
    }
}