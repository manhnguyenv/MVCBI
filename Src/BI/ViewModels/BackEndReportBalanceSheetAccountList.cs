using BI.Helpers;
using BI.Models;
using BI.Models.POCO;
using System;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndReportBalanceSheetAccountList
  {
    public List<DmDvcs> DmDvcss { get; set; }

    [DataAnnotationsDisplay("MaDonVi")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string MaDonVi { get; set; }

    [DataAnnotationsDisplay("StartDate")]
    public string StartDate { get; set; }

    [DataAnnotationsDisplay("EndDate")]
    public string EndDate { get; set; }

    public List<ReportBalanceSheetAccount> ReportBalanceSheetAccounts { get; set; }

    public BackEndReportBalanceSheetAccountList()
    {
      // SET DATEFORMAT DMY;
      // EXEC sp_glcd2 '01/01/2018','30/07/2018','04'

      //MaDonVi = "04";

      var dtNow = DateTime.Now;
      StartDate = DateTimeHelper.GetFirstDayOfMonth(dtNow).ToString("dd/MM/yyyy");
      EndDate = DateTimeHelper.GetLastDayOfMonth(dtNow).ToString("dd/MM/yyyy");
    }
  }
}
