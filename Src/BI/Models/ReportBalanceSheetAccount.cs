using AutoMapper;
using BI.Helpers;
using BI.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BI.Models
{
  public class ReportBalanceSheetAccount
  {
    public int? Stt { get; set; }
    public string Status { get; set; }
    public string Mark { get; set; }
    public string Tk { get; set; }
    public string Ten_tk { get; set; }
    public string Ten_tk2 { get; set; }
    public string Loai_tk { get; set; }
    public string Bac_tk { get; set; }
    public string Tk_sc { get; set; }
    public string Tk_cn { get; set; }
    public decimal? No_dk { get; set; }
    public decimal? Co_dk { get; set; }
    public decimal? No_dk_nt { get; set; }
    public decimal? Co_dk_nt { get; set; }
    public decimal? Ps_no { get; set; }
    public decimal? Ps_co { get; set; }
    public decimal? Ps_no_nt { get; set; }
    public decimal? Ps_co_nt { get; set; }
    public decimal? Ps_no_lk { get; set; }
    public decimal? Ps_co_lk { get; set; }
    public decimal? Ps_no_ntLk { get; set; }
    public decimal? Ps_co_ntLk { get; set; }
    public decimal? No_ck { get; set; }
    public decimal? Co_ck { get; set; }
    public decimal? No_ck_nt { get; set; }
    public decimal? Co_ck_nt { get; set; }

    public string Ten_tk_UTF8
    {
      get
      {
        var tenTk = "";
        tenTk = DataHelper.ConvertTCVN3ToUTF8(this.Ten_tk);
        return tenTk;
      }
    }

    public string Ps_no_fm
    {
      get
      {
        string psNo = "";
        if (this.Ps_no.HasValue) psNo = DataHelper.FormatDecimal(this.Ps_no.Value);
        return psNo;
      }
    }

    public string Ps_co_fm
    {
      get
      {
        string psCo = "";
        if (this.Ps_co.HasValue) psCo = DataHelper.FormatDecimal(this.Ps_co.Value);
        return psCo;
      }
    }

    public string No_dk_fm
    {
      get
      {
        string noDk = "";
        if (this.No_dk.HasValue) noDk = DataHelper.FormatDecimal(this.No_dk.Value);
        return noDk;
      }
    }

    public string Co_dk_fm
    {
      get
      {
        string coDk = "";
        if (this.Co_dk.HasValue) coDk = DataHelper.FormatDecimal(this.Co_dk.Value);
        return coDk;
      }
    }

    public string Co_ck_fm
    {
      get
      {
        string coCk = "";
        if (this.Co_ck.HasValue) coCk = DataHelper.FormatDecimal(this.Co_ck.Value);
        return coCk;
      }
    }

    public string No_ck_fm
    {
      get
      {
        string noCk = "";
        if (this.No_ck.HasValue) noCk = DataHelper.FormatDecimal(this.No_ck.Value);
        return noCk;
      }
    }

    public bool? Level1MakeBold
    {
      get
      {
        return Bac_tk == "1";
      }
    }
  }

  public class ReportBalanceSheetAccountModel
  {
    public ReportBalanceSheetAccountModel()
    {
    }

    public List<ReportBalanceSheetAccount> GetAll(
        string maDonVi,
        string startDate,
        string endDate
        )
    {
      List<ReportBalanceSheetAccount> result = new List<ReportBalanceSheetAccount>();

      var dBg = DataHelper.GetDateFromNgayThangNam(startDate);
      var dTo = DataHelper.GetDateFromNgayThangNam(endDate);

      var dictParams = new Dictionary<string, object>
            {
                { "dBg", dBg.ToShortDateString()},
                { "dTo", dTo.ToShortDateString()},
                { "cUnit", maDonVi }
            };

      // SET DATEFORMAT DMY;
      // EXEC sp_glcd2 '01/01/2018','30/07/2018','04'
      var list = new StoredProcedureFactory().GetListBy<sp_Glcd2>(dictParams, "sp_glcd2").ToList();
      result = Mapper.Map<List<sp_Glcd2>, List<ReportBalanceSheetAccount>>(list);

      return result;
    }
  }
}
