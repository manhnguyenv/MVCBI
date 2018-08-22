using System;
using System.Runtime.Serialization;

namespace BI.Infrastructure
{
  public class InvalidExportDataException : Exception, ISerializable
  {
    public InvalidExportDataException()
        : base() { }

    public InvalidExportDataException(string message)
        : base(message) { }

    public InvalidExportDataException(string format, params object[] args)
        : base(string.Format(format, args)) { }

    public InvalidExportDataException(string message, Exception innerException)
        : base(message, innerException) { }

    public InvalidExportDataException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

    protected InvalidExportDataException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
  }
}
