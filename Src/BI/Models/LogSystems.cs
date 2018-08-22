using BI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BI.Models
{
  public class LogSystem
  {
    public int id { get; set; }
    public int? user_id { get; set; }
    public string username { get; set; }
    public int? type { get; set; }
    public string description { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public DateTime? deleted_at { get; set; }
    public string record_id { get; set; }
    public string table_name { get; set; }
    public DateTime? created_on { get; set; }

    public string Name
    {
      get { return string.IsNullOrWhiteSpace(username) ? (user_id.HasValue ? user_id.Value.ToString() : string.Empty) : username; }
    }

    public string TypeName
    {
      get
      {
        string typeName = string.Empty;
        int typeInt = type.HasValue ? type.Value : 0;
        switch (typeInt)
        {
          case 1:
            typeName = "Tạo mới";
            break;

          case 2:
            typeName = "Cập nhật";
            break;

          case 3:
            typeName = "Xóa";
            break;

          default:
            typeName = "";
            break;
        }
        return typeName;
      }
    }
  }

  public class LogSystems
  {
    private List<LogSystem> _allItems;

    private List<LogSystem> GetAllItems()
    {
      return AdoHelper.ExecCachedListProc<LogSystem>("sp_cms_LogSystem_select", true);
    }

    public LogSystems()
    {
      _allItems = GetAllItems();
    }

    public List<LogSystem> GetAll()
    {
      List<LogSystem> result = new List<LogSystem>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  select i).ToList();
      }

      return result;
    }

    public LogSystem FilterById(int? id)
    {
      LogSystem result = null;

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where i.id == id
                  select i).FirstOrDefault();
      }

      return result;
    }

    public int? AddEdit(
        int? id,
int? user_id,
string username,
int? type,
string description,
DateTime? created_at,
DateTime? updated_at,
DateTime? deleted_at
        )
    {
      int? result;
      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_LogSystem_insert_update",
            "@id", id,
            "@user_id", user_id,
            "@username", username,
            "@type", type,
            "@description", description,
            "@created_at", created_at,
            "@updated_at", updated_at,
            "@deleted_at", deleted_at,
            returnValue);
        result = db.GetParamReturnValue(returnValue);
        return result;
      }
    }

    public int? Delete(int? id)
    {
      int? result;

      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_LogSystem_delete", "@Id", id, returnValue);
        result = db.GetParamReturnValue(returnValue);
      }

      return result;
    }

    public List<LogSystem> GetAllUserIds()
    {
      List<LogSystem> result = new List<LogSystem>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where (i != null && i.user_id != null && i.user_id.HasValue)
                  select i).Distinct(x => x.user_id).ToList();
      }

      return result;
    }

    public List<LogSystem> GetAllUserNames()
    {
      List<LogSystem> result = new List<LogSystem>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where (i != null && i.username != null)
                  select i).Distinct(x => x.username).ToList();
      }

      return result;
    }
  }
}
