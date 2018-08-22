using BI.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BI.Models
{
  public class SysVar
  {
    public string Sys_Id { get; set; }
    public string Sys_Var { get; set; }
    public string Sys_Type { get; set; }
    public decimal? Sys_Value { get; set; }
    public string Sys_Name { get; set; }
    public bool IsEditable { get; set; }
  }

  public class SysVars
  {
    private List<SysVar> _allItems;

    private List<SysVar> GetAllItems()
    {
      return AdoHelper.ExecCachedListProc<SysVar>("sp_cms_SysVar_select", true);
    }

    public SysVars()
    {
      _allItems = GetAllItems();
    }

    public List<SysVar> GetAll()
    {
      List<SysVar> result = new List<SysVar>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  select i).ToList();
      }

      return result;
    }

    public SysVar FilterById(string id)
    {
      SysVar result = null;

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where i.Sys_Id == id
                  select i).FirstOrDefault();
      }

      return result;
    }

    public int? AddEdit(string Sys_Id,
string Sys_Var,
string Sys_Type,
decimal? Sys_Value,
string Sys_Name,
bool? IsEditable,
string username)
    {
      int? result;
      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_SysVar_insert_update",
            "@Sys_Id", Sys_Id,
            "@Sys_Var", Sys_Var,
            "@Sys_Type", Sys_Type,
            "@Sys_Name", Sys_Name,
            "@Sys_Value", Sys_Value,
            "@IsEditable", IsEditable,
            "@UserName", username,
            returnValue);
        result = db.GetParamReturnValue(returnValue);
        return result;
      }
    }

    public int? Delete(string id, string username)
    {
      int? result;

      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_SysVar_delete", "@Id", id, "@UserName", username, returnValue);
        result = db.GetParamReturnValue(returnValue);
      }

      return result;
    }
  }
}
