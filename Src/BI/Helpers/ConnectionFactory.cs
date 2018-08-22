using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BI.Helpers
{
  public class ConnectionFactory : IDisposable
  {
    public static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["SM17ConnectionString"].ConnectionString;

    public static IDbConnection GetInstance()
    {
      IDbConnection connection = new SqlConnection(ConnectionString);
      return connection;
    }

    public void Dispose()
    {
      this.Dispose();
    }
  }
}
