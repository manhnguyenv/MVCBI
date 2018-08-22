using BI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BI.Models
{
  public class Tenant
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameEn { get; set; }
    public string MST { get; set; }
    public string Dvcs { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public string Representative { get; set; }
    public DateTime? DateIssue { get; set; }
    public DateTime? DateActive { get; set; }
    public string Domain { get; set; }
    public string Address { get; set; }

    /// <summary>
    /// Dùng để tạo connection string
    /// </summary>
    public string ServerName { get; set; }

    public string DbName { get; set; }
    public string DbUserName { get; set; }
    public string DbPassword { get; set; }
    public string DbPort { get; set; }
  }

  public class Tenants
  {
    private List<Tenant> _AllItems;

    private List<Tenant> GetAllItems(bool force = false)
    {
      return AdoHelper.ExecCachedListProc<Tenant>("sp_admin_tenants_select", force);
    }

    public Tenants()
    {
      _AllItems = GetAllItems(true);
    }

    public List<Tenant> GetAll()
    {
      List<Tenant> result = new List<Tenant>();

      if (_AllItems.IsNotNull())
      {
        result = (from i in _AllItems
                  select i).ToList();
      }

      return result;
    }

    public List<Tenant> GetAllTenants(string userName = null, string fullName = null, string email = null, int? groupId = null)
    {
      List<Tenant> result = null;

      if (_AllItems.IsNotNull())
      {
        result = (from i in _AllItems
                  select i).ToList();
      }

      return result;
    }

    public Tenant FilterById(int? id)
    {
      Tenant result = null;

      if (_AllItems.IsNotNull())
      {
        result = (from i in _AllItems
                  where i.Id == id
                  select i).FirstOrDefault();
      }

      return result;
    }

    public int? AddEdit(int? id,
            string mst,
            string dvcs,
            string name,
            string nameEn,
            string email,
            string phone,
            string representative,
            string domain,
            string address,
            string serverName,
            string dbName,
            string dbUserName,
            string dbPassword,
            string dbPort,
            DateTime? dateIssue,
            DateTime? dateActive,
            string username)
    {
      int? result;
      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_tenant_insert_update",
            "@Id", id,
            "@MST", mst,
            "@Dvcs", dvcs,
            "@Name", name,
            "@NameEn", nameEn,
            "@Email", email,
            "@Phone", phone,
            "@Representative", representative,
            "@Domain", domain,
            "@Address", address,
            "@ServerName", serverName,
            "@DbName", dbName,
            "@DbUserName", dbUserName,
            "@DbPassword", dbPassword,
            "@DbPort", dbPort,
            "@DateIssue", dateIssue,
            "@DateActive", dateActive,
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
        db.ExecNonQueryProc("sp_cms_tenant_delete", "@Id", id, "@UserName", username, returnValue);
        result = db.GetParamReturnValue(returnValue);
      }

      return result;
    }
  }
}
