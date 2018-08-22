using System;
using System.Collections.Generic;
using VASJ.BI.Helpers;
using VASJ.BI.Models;
using VASJ.BI.Models.POCO;

namespace VASJ.BI.ViewModels
{
    public class BackEndReportBalanceSheetList
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

        public List<ReportBalanceSheet> ReportBalanceSheets { get; set; }

        public BackEndReportBalanceSheetList()
        {
            // SET DATEFORMAT DMY;
            // EXEC sp_arcd1 '131%','%','01/01/2018','30/06/2018','04'

            //UserId = "131";
            //CustomerId = "";
            //MaDonVi = "04";

            var dtNow = DateTime.Now;
            StartDate = DateTimeHelper.GetFirstDayOfMonth(dtNow).ToString("dd/MM/yyyy");
            EndDate = DateTimeHelper.GetLastDayOfMonth(dtNow).ToString("dd/MM/yyyy");
        }
    }
}
