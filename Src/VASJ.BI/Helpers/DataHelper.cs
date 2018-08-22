using System;
using System.Globalization;
using VASJ.BI.Helpers;
using VASJ.BI.Models;

namespace VASJ.BI
{
  public class DataHelper
  {
    public static DateTime GetDate(string dateString, string dateFormat)
    {
      try
      {
        DateTime.TryParseExact(dateString,
                           dateFormat, //Example MM/dd/yyyy, dd/MM/yyyy
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out DateTime dt);
        return DateTime.MinValue == dt ? DateTimeExt.SQL_DEFAULT : dt;
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return DateTimeExt.SQL_DEFAULT;
      }
    }

    public static DateTime GetDate(string dateString)
    {
      //This should work based on your example "2011-29-01 12:00 am"
      DateTime.TryParseExact(dateString,
                             "yyyy-dd-MM hh:mm tt",
                             CultureInfo.InvariantCulture,
                             DateTimeStyles.None,
                             out DateTime dt);
      return dt;
    }

    public static string BuildDynamicConnectionString(string connString, Tenant tenant)
    {
      try
      {
        return "Data Source=" + tenant.ServerName + ";" +
             "Initial Catalog=" + tenant.DbName + ";" +
             "Persist Security Info=True;" +
             "User ID=" + tenant.DbUserName + ";" +
             "Password=" + SecurityHelper.Decrypt(tenant.DbPassword) + ";";
      }
      catch
      {
        return connString;
      }
    }

    public static DateTime GetDateFromNgayThangNam(string dateString)
    {
      return string.IsNullOrWhiteSpace(dateString) ? DateTime.Now.AddDays(-31) : DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    public static DateTime GetDateFromNgayThangNamGioPhut(string dateString)
    {
      return string.IsNullOrWhiteSpace(dateString) ? DateTime.Now.AddDays(-31) : DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    public static decimal? GetDecimal(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return null;
      try
      {
        return Convert.ToDecimal(value);
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return null;
      }
    }

    public static string ConvertTCVN3ToUTF8(string tcvn3String)
    {
      return Converter.TCVN3ToUnicode(tcvn3String);
    }

    public static DateTime? GetDateTime(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return null;
      try
      {
        return Convert.ToDateTime(value);
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return null;
      }
    }

    public static string FormatDecimal(decimal gold)
    {
      CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
      string goldFormat = gold
          .ToString("#,###", cul.NumberFormat)
          .Replace(".", ",");
      return goldFormat;
    }

    public static bool? GetBoolean(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return null;

      try
      {
        if (value.ToUpper().Equals("1") || value.ToUpper().Equals("TRUE"))
          return true;
        else
          return Convert.ToBoolean(value);
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return null;
      }
    }

    public static decimal GetDecimal(string gold, decimal defaultValue)
    {
      if (string.IsNullOrWhiteSpace(gold)) return 0;

      try
      {
        return Convert.ToDecimal(gold);
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return 0;
      }
    }

    public static long GetLong(string gold)
    {
      if (string.IsNullOrWhiteSpace(gold)) return 0;

      try
      {
        return Convert.ToUInt32(gold);
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
        return 0;
      }
    }
  }
}
