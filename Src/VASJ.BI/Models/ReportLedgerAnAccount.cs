using System;

namespace VASJ.BI.Models
{
    public class ReportLedgerAnAccount
    {
        public string Stt_rec { get; set; }
        public decimal? Stt_ct_nkc { get; set; }
        public DateTime? Ngay_ct { get; set; }
        public string So_ct { get; set; }
        public string Ma_vv { get; set; }
        public string Tk { get; set; }
        public string Tk_du { get; set; }
        public decimal? Ps_no { get; set; }
        public decimal? Ps_co { get; set; }
        public string Ma_nt { get; set; }
        public decimal? Ty_gia { get; set; }
        public decimal? Ps_no_nt { get; set; }
        public decimal? Ps_co_nt { get; set; }
        public string Dien_giai { get; set; }
        public string Nh_Dk { get; set; }
        public int Soft { get; set; }

        public string Dien_giai_UTF8
        {
            get
            {
                var dienGiai = "";
                dienGiai = DataHelper.ConvertTCVN3ToUTF8(this.Dien_giai);
                return dienGiai;
            }
        }

        public string Ten_Kh_UTF8
        {
            get
            {
                var tenKh = "";
                tenKh = DataHelper.ConvertTCVN3ToUTF8(this.Ten_Kh);
                return tenKh;
            }
        }

        public string Ma_ct { get; set; }
        public string Ma_Kh { get; set; }
        public string Ma_Kh2 { get; set; }
        public string Ten_Kh { get; set; }

        public ReportLedgerAnAccount()
        {
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
    }
}