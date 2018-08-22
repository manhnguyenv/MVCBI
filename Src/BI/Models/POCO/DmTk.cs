using System;
using VASJ.BI.Helpers;

namespace VASJ.BI.Models.POCO
{
    public class DmTk
    {
        public string Ma_Dvcs { get; set; }
        public string Tk { get; set; }
        public string Ten_Tk { get; set; }
        public string Ten_Tk2 { get; set; }
        public string Ten_Ngan { get; set; }
        public string Ten_Ngan2 { get; set; }
        public string Ma_Nt { get; set; }
        public decimal Loai_Tk { get; set; }
        public string Tk_Me { get; set; }
        public decimal Bac_Tk { get; set; }
        public decimal Tk_Sc { get; set; }
        public decimal Tk_Cn { get; set; }
        public string time { get; set; }
        public decimal user_id { get; set; }
        public string time0 { get; set; }
        public decimal user_id0 { get; set; }
        public string status { get; set; }
        public DateTime? DATE { get; set; }
        public DateTime? DATE0 { get; set; }

        public string Ten_Tk_UTF8
        {
            get
            {
                var maTk = this.Tk;

                var tenTk = "";
                tenTk = Converter.TCVN3ToUnicode(this.Ten_Tk);

                return $"{maTk} --- {tenTk}";
            }
        }
    }
}