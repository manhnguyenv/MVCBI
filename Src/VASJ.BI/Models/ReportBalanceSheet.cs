using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VASJ.BI.Helpers;
using VASJ.BI.Models.DTO;

namespace VASJ.BI.Models
{
    public class ReportBalanceSheet
    {
        public string Stt { get; set; }
        public string fTag { get; set; }
        public string fSort { get; set; }
        public string ma_kh { get; set; }
        public string Ten_kh { get; set; }
        public string Ten_kh2 { get; set; }
        public string Nh_kh1 { get; set; }
        public string Nh_kh2 { get; set; }
        public string Nh_kh3 { get; set; }
        public decimal? No_dk { get; set; }
        public decimal? Co_dk { get; set; }
        public decimal? No_dk_nt { get; set; }
        public decimal? Co_dk_nt { get; set; }
        public decimal? Ps_no { get; set; }
        public decimal? Ps_co { get; set; }
        public decimal? Ps_no_nt { get; set; }
        public decimal? Ps_co_nt { get; set; }
        public decimal? No_ck { get; set; }
        public decimal? Co_ck { get; set; }
        public decimal? No_ck_nt { get; set; }
        public decimal? Co_ck_nt { get; set; }

        public string Ten_kh_UTF8
        {
            get
            {
                var tenKH = "";
                tenKH = DataHelper.ConvertTCVN3ToUTF8(this.Ten_kh);
                return tenKH;
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
    }

    public class ReportBalanceSheetModel
    {
        public ReportBalanceSheetModel()
        {
        }

        public List<ReportBalanceSheet> GetAll(
            string userId,
            string customerId,
            string maDonVi,
            string startDate,
            string endDate
            )
        {
            List<ReportBalanceSheet> result = new List<ReportBalanceSheet>();

            var dBg = DataHelper.GetDateFromNgayThangNam(startDate);
            var dTo = DataHelper.GetDateFromNgayThangNam(endDate);

            var dictParams = new Dictionary<string, object>
            {
                { "cAcc", userId },
                { "cCust", customerId },
                { "dBg", dBg.ToShortDateString()},
                { "dTo", dTo.ToShortDateString()},
                { "cUnit", maDonVi }
            };

            // SET DATEFORMAT DMY;
            // EXEC sp_arcd1 '131%','%','01/01/2018','30/06/2018','04'
            var list = new StoredProcedureFactory().GetListBy<sp_Arcd1>(dictParams, "sp_arcd1").ToList();
            result = Mapper.Map<List<sp_Arcd1>, List<ReportBalanceSheet>>(list);

            return result;
        }
    }
}