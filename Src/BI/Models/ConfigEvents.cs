using BI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BI.Models
{
  public class ConfigEvent
  {
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int FinishGame { get; set; }
    public int Status { get; set; }
  }

  public class ConfigEvents
  {
    private List<ConfigEvent> _allItems;

    private List<ConfigEvent> GetAllItems()
    {
      return AdoHelper.ExecCachedListProc<ConfigEvent>("sp_cms_config_events_select", true);
    }

    public ConfigEvents()
    {
      _allItems = GetAllItems();
    }

    public List<ConfigEvent> GetAll()
    {
      List<ConfigEvent> result = new List<ConfigEvent>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  select i).ToList();
      }

      return result;
    }

    public ConfigEvent FilterById(int? id)
    {
      ConfigEvent result = null;

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where i.Id == id
                  select i).FirstOrDefault();
      }

      return result;
    }

    public int? AddEdit(int? id, string eventName, DateTime? startDate, DateTime? endDate, int status, string username)
    {
      int? result;
      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_config_event_insert_update",
            "@Id", id,
            "@EventName", eventName,
            "@StartDate", startDate,
            "@EndDate", endDate,
            "@Status", status,
            "@UserName", username,
            returnValue);
        result = db.GetParamReturnValue(returnValue);
        return result;
      }
    }

    public int? Delete(int? id, string username)
    {
      int? result;

      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_config_event_delete", "@Id", id, "@UserName", username, returnValue);
        result = db.GetParamReturnValue(returnValue);
      }

      return result;
    }
  }
}
