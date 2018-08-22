using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace VASJ.BI.Helpers
{
  public class StoredProcedureFactory
  {
    public string Execute(Dictionary<string, object> parameters, string spname)
    {
      var error = "";
      using (var con = ConnectionFactory.GetInstance())
      {
        con.Open();
        var param = new DynamicParameters();
        foreach (var key in parameters.Keys)
        {
          param.Add(key, parameters[key]);
        }
        param.Add("Error", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
        con.Execute(spname, param, commandType: CommandType.StoredProcedure);
        error = param.Get<string>("Error");
      }
      return error;
    }

    public IEnumerable<T> GetList<T>(string spname)
    {
      IEnumerable<T> data;
      using (var con = ConnectionFactory.GetInstance())
      {
        con.Open();
        data = con.Query<T>(spname, commandType: CommandType.StoredProcedure);
      }
      return data;
    }

    public T GetOneBy<T>(Dictionary<string, object> parameters, string spname)
    {
      T entity;
      using (var con = ConnectionFactory.GetInstance())
      {
        var param = new DynamicParameters();
        foreach (var key in parameters.Keys)
        {
          param.Add(key, parameters[key]);
        }
        con.Open();
        entity = con.Query<T>(spname, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
      }
      return entity;
    }

    public IEnumerable<T> GetListBy<T>(Dictionary<string, object> parameters, string spname)
    {
      IEnumerable<T> data;
      using (var con = ConnectionFactory.GetInstance())
      {
        var param = new DynamicParameters();
        foreach (var key in parameters.Keys)
        {
          param.Add(key, parameters[key]);
        }
        con.Open();
        data = con.Query<T>(spname, param, commandType: CommandType.StoredProcedure);
      }
      return data;
    }
  }
}
