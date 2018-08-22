namespace BI.Models
{
  public class ReportDebtDetailSummary
  {
    public string Tk { get; set; }
    public string Ten_Tk { get; set; }
    public decimal Du_No1 { get; set; }
    public decimal Du_Co1 { get; set; }
    public decimal Ps_No { get; set; }
    public decimal Ps_Co { get; set; }
    public decimal Du_No2 { get; set; }
    public decimal Du_Co2 { get; set; }
    public decimal Du_No_Nt1 { get; set; }
    public decimal Du_Co_Nt1 { get; set; }
    public decimal Ps_No_Nt { get; set; }
    public decimal Ps_Co_Nt { get; set; }
    public decimal Du_No_Nt2 { get; set; }
    public decimal Du_Co_Nt2 { get; set; }

    // Công thức tính số dư đầu kỳ và số dư cuối kỳ
    // của báo cáo công nợ
    //nDu_dau_ky = (m.Du_no1 + m.Du_co1)
    //nDu_dau_ky_nt = (m.Du_no_nt1 + m.Du_co_nt1)
    //nDu_cuoi_ky = (Du_no2 + Du_co2)
    //nDu_cuoi_ky_nt = (m.Du_no_nt2 + m.Du_co_nt2)

    public decimal Du_dau_ky
    {
      get
      {
        return this.Du_No1 + this.Du_Co1;
      }
    }

    public decimal Du_dau_ky_nt
    {
      get
      {
        return this.Du_No_Nt1 + this.Du_Co_Nt1;
      }
    }

    public decimal Du_cuoi_ky
    {
      get
      {
        return this.Du_No2 + this.Du_Co2;
      }
    }

    public decimal Du_cuoi_ky_nt
    {
      get
      {
        return this.Du_No_Nt2 + this.Du_Co_Nt2;
      }
    }

    //m.Ten_so_du_dk=Iif(nDu_dau_ky>0,"do no dau ky ", "du co dau ky")
    //m.Ten_so_du_ck=Iif(nDu_cuoi_ky>0,"du no cuoi ky ","Du co cuoi ky ")
    public string Ten_so_du_dk
    {
      get
      {
        return this.Du_dau_ky > 0 ? "Dư nợ đầu kỳ" : "Dư có đầu kỳ";
      }
    }

    public string Ten_so_du_ck
    {
      get
      {
        return this.Du_cuoi_ky > 0 ? "Dư nợ cuối kỳ" : "Dư có cuối kỳ";
      }
    }
  }
}
