using BI.Helpers;
using BI.Models;
using BI.Models.POCO;
using System;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndReportDebtDetailList
  {
    public List<DmKh> DmKhs { get; set; }

    public List<DmTk> DmTks { get; set; }

    public List<DmDvcs> DmDvcss { get; set; }

    [DataAnnotationsDisplay("TaiKhoan")]
    public string UserId { get; set; }

    [DataAnnotationsDisplay("MaKhach")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string CustomerId { get; set; }

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

    [DataAnnotationsDisplay("ChiTietTheoHH")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string ChiTietTheoHH { get; set; }

    [DataAnnotationsDisplay("MauBaoCao")]
    public bool? MauBaoCao { get; set; }

    [DataAnnotationsDisplay("NgoaiTe")]
    public bool? NgoaiTe { get; set; }

    public ReportDebtDetailViewModel ReportDebtDetails { get; set; }

    public BackEndReportDebtDetailList()
    {
      DmKhs = new List<DmKh>();
      DmTks = new List<DmTk>();
      DmDvcss = new List<DmDvcs>();

      // SET DATEFORMAT DMY;
      // EXEC sp_arso1 '131%','%','01/01/2018','30/06/2018','04'

      //UserId = "131";
      //CustomerId = "";
      //MaDonVi = "04";

      var dtNow = DateTime.Now;
      DateTime firstDayCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);
      DateTime firstDayOfMonth = DateTimeHelper.GetFirstDayOfMonth(dtNow);
      StartDate = firstDayCurrentYear.ToString("dd/MM/yyyy");
      EndDate = DateTimeHelper.GetLastDayOfMonth(dtNow).ToString("dd/MM/yyyy");
    }
  }
}
