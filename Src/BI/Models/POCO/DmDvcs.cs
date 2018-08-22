using BI.Helpers;
using System;

namespace BI.Models.POCO
{
  public class DmDvcs
  {
    public string Ma_Dvcs { get; set; }
    public string Ten_Dvcs { get; set; }
    public string Ten_Dvcs2 { get; set; }
    public string Dia_Chi { get; set; }
    public string Time { get; set; }
    public decimal User_Id { get; set; }
    public string Time0 { get; set; }
    public decimal User_Id0 { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public DateTime DAte0 { get; set; }
    public string Tag { get; set; }
    public string Ws_Id { get; set; }
    public string Len_Th { get; set; }
    public string Ma_Dvcs_Me { get; set; }
    public string Gr_Id { get; set; }

    public string Ten_Dvcs_UTF8
    {
      get
      {
        var tenDvcs = "";

        tenDvcs = Converter.TCVN3ToUnicode(this.Ten_Dvcs);

        return $"{Ma_Dvcs} --- {tenDvcs}";
      }
    }
  }
}
