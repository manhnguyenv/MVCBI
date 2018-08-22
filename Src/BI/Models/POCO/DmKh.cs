using System;
using VASJ.BI.Helpers;

namespace VASJ.BI.Models.POCO
{
    public class DmKh
    {
        public string Ma_Dvcs { get; set; }
        public string Ma_Kh { get; set; }
        public string Ten_Kh { get; set; }
        public string Ten_Kh2 { get; set; }
        public string Loai_Kh { get; set; }
        public string Ma_So_Thue { get; set; }
        public string Dia_Chi { get; set; }
        public string Dien_Thoai { get; set; }
        public string Fax { get; set; }
        public string E_Mail { get; set; }
        public string Home_Page { get; set; }
        public string Doi_Tac { get; set; }
        public string Ong_Ba { get; set; }
        public string Ten_Bp { get; set; }
        public string Ten_Nh { get; set; }
        public string Ghi_Chu { get; set; }
        public string Nh_Kh1 { get; set; }
        public string Nh_Kh2 { get; set; }
        public string Nh_Kh3 { get; set; }
        public string Tinh_Thanh { get; set; }
        public string Time { get; set; }
        public decimal User_Id { get; set; }
        public string Time0 { get; set; }
        public decimal User_Id0 { get; set; }
        public DateTime Date { get; set; }
        public DateTime Date0 { get; set; }
        public string Status { get; set; }

        public string Ten_Kh_UTF8
        {
            get
            {
                var tenKh = "";
                tenKh = Converter.TCVN3ToUnicode(this.Ten_Kh);
                var ten = $"{Ma_Kh} --- {tenKh}";
                return string.IsNullOrWhiteSpace(this.Dia_Chi) ? ten : $"{ten} --- {Converter.TCVN3ToUnicode(this.Dia_Chi)}";
            }
        }
    }
}