using BI.Helpers;
using BI.Models;
using BI.Models.POCO;
using System;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndReportLedgerAnAccountList
  {
    public List<DmTk> DmTks { get; set; }

    public List<DmDvcs> DmDvcss { get; set; }

    [DataAnnotationsDisplay("TaiKhoan")]
    public string UserId { get; set; }

    [DataAnnotationsDisplay("MaDonVi")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string MaDonVi { get; set; }

    [DataAnnotationsDisplay("StartDate")]
    public string StartDate { get; set; }

    [DataAnnotationsDisplay("EndDate")]
    public string EndDate { get; set; }

    //[DataAnnotationsDisplay("H")]
    //[RegularExpression(@"^\d+(\.\d{0,3})?$", ErrorMessage = "Trường này không thể chứa nhiều hơn 3 chữ số sau dấu chấm.")]
    //[Range(0.001, 1000, ErrorMessage = "Trường này phải nằm trong khoảng từ 0.001 đến 1000.")]
    //public decimal? Amount { get; set; }

    [DataAnnotationsDisplay("MauBaoCao")]
    public bool? MauBaoCao { get; set; }

    [DataAnnotationsDisplay("NgoaiTe")]
    public bool? NgoaiTe { get; set; }

    public ReportLedgerAnAccountViewModel ReportLedgerAnAccounts { get; set; }

    public BackEndReportLedgerAnAccountList()
    {
      DmTks = new List<DmTk>();
      DmDvcss = new List<DmDvcs>();

      // SET DATEFORMAT DMY;
      // EXEC usp_Nkc_SoCaiMotTaiKhoanBak
      //   @_DocDate1 = '01/01/2018',
      //   @_DocDate2 = '10/01/2018',
      //   @_Account = '111',
      //   @_BranchCode = '04',
      //   @_IsCurrency = 1

      //UserId = "111";
      //MaDonVi = "04";

      var dtNow = DateTime.Now;
      DateTime firstDayCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);
      DateTime firstDayOfMonth = DateTimeHelper.GetFirstDayOfMonth(dtNow);
      StartDate = firstDayCurrentYear.ToString("dd/MM/yyyy");
      EndDate = DateTimeHelper.GetLastDayOfMonth(dtNow).ToString("dd/MM/yyyy");
    }
  }
}
