using BI.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BI.Models
{
  public class TransactionLog
  {
    public long Id { get; set; }
    public Guid Id_Fighting { get; set; }
    public DateTime? TransationDate { get; set; }
    public string ReceiveAccount { get; set; }
    public int? TransationType { get; set; }

    public string TransationTypeDesc
    {
      get
      {
        var desc = "";
        switch (this.TransationType)
        {
          case 4:
            desc = "Đăng ký đặt cược";
            break;

          case 5:
            desc = "Đăng ký nhận cược";
            break;

          default:
            break;
        }
        return desc;
      }
    }

    public decimal? Amount { get; set; }

    public string AmountFm
    {
      get
      {
        double goldNum = (Amount.HasValue ? Convert.ToDouble(Amount.Value) : 0);
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
        string result = goldNum.ToString("#,###", cul.NumberFormat);
        return result;
      }
    }

    public string UserId { get; set; }
    public string Nick { get; set; }
    public string Reason { get; set; }
    public bool Status { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime? Created_At { get; set; }
    public DateTime? Updated_At { get; set; }
  }

  public class TransactionLogs
  {
    private List<TransactionLog> _allItems;

    private List<TransactionLog> GetAllItems()
    {
      return AdoHelper.ExecCachedListProc<TransactionLog>("sp_cms_TransactionLog_select", true);
    }

    public TransactionLogs()
    {
      _allItems = GetAllItems();
    }

    public List<TransactionLog> GetAll(
            string tranDate,
            string userId,
            string nick,
            string reason
        )
    {
      List<TransactionLog> result = new List<TransactionLog>();

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where (userId.IsNull() || i.UserId.Contains(userId, StringComparison.OrdinalIgnoreCase))
                      && (nick.IsNull() || i.Nick.Contains(nick, StringComparison.OrdinalIgnoreCase))
                      && (reason.IsNull() || i.Reason.Contains(reason, StringComparison.OrdinalIgnoreCase))
                      && (tranDate.IsNull() || i.TransationDate.ToDateTimeString().Contains(tranDate, StringComparison.OrdinalIgnoreCase))
                  select i).ToList();
      }

      return result;
    }

    public TransactionLog FilterById(long? id)
    {
      TransactionLog result = null;

      if (_allItems.IsNotNull())
      {
        result = (from i in _allItems
                  where i.Id == id
                  select i).FirstOrDefault();
      }

      return result;
    }

    public int? AddEdit(
               long? Id,
               Guid ID_Fighting,
               DateTime? TransationDate,
               string ReceiveAccount,
               int? TransationType,
               decimal? Amount,
               string UserId,
               string Nick,
               string Reason,
               bool? Status,
               string ErrorMessage,
               DateTime? Created_At,
               DateTime? Updated_At,
               string UserName
        )
    {
      int? result;
      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_TransactionLog_insert_update",
            "@Id", Id,
            "@ID_Fighting", ID_Fighting,
            "@TransationDate", TransationDate,
            "@ReceiveAccount", ReceiveAccount,
            "@TransationType", TransationType,
            "@Amount", Amount,
            "@UserId", UserId,
            "@Nick", Nick,
            "@Reason", Reason,
            "@Status", Status,
            "@ErrorMessage", ErrorMessage,
            "@Created_At", Created_At,
            "@Updated_At", Updated_At,
            "@UserName", UserName,
            returnValue);
        result = db.GetParamReturnValue(returnValue);
        return result;
      }
    }

    public int? Delete(long? id, string username)
    {
      int? result;

      using (AdoHelper db = new AdoHelper())
      {
        var returnValue = db.CreateParamReturnValue("returnValue");
        db.ExecNonQueryProc("sp_cms_TransactionLog_delete", "@Id", id, "@UserName", username, returnValue);
        result = db.GetParamReturnValue(returnValue);
      }

      return result;
    }
  }
}
