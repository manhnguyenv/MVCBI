using System;

namespace BI.Helpers
{
  public class SqlDataReaderExt
  {
    public static T CheckNull<T>(object obj)
    {
      return (obj == DBNull.Value ? default(T) : (T)obj);
    }

    public static DateTime CheckNullDate(object obj, string dateFormat)
    {
      if (obj == DBNull.Value) return DateTimeExt.SQL_DEFAULT;
      return DataHelper.GetDate(obj.ToString(), dateFormat);
    }
  }
}
