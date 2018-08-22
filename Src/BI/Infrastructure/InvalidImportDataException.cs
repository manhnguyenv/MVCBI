using System;
using System.Runtime.Serialization;

namespace VASJ.BI.Infrastructure
{
    public class InvalidImportDataException : Exception, ISerializable
    {
        public InvalidImportDataException()
            : base() { }

        public InvalidImportDataException(string message)
            : base(message) { }

        public InvalidImportDataException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InvalidImportDataException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidImportDataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InvalidImportDataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}