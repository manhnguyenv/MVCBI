using AutoMapper;
using BI.Helpers;
using BI.Models;
using BI.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BI.Controllers
{
  public class DefaultController : Controller
  {
    // GET: /Default/Index
    public ActionResult Index(string message = "")
    {
      return RedirectToAction("Login", "Admin");
    }

    private List<ReportDebtDetail> GetReportDebtDetails()
    {
      var RC = 1;
      var cAcc = "131%";
      var cCust = "352NAMHUNG%";
      var dBg = "01/01/2018";
      var dTo = "31/12/2018";
      var cUnit = "04%";

      var dictParams = new Dictionary<string, object>
            {
                { "cAcc", cAcc },
                { "cCust", cCust},
                { "cUnit", cUnit },
                { "dBg", dBg },
                { "dTo", dTo },
                { "RC", RC }
            };
      var list = new StoredProcedureFactory().GetListBy<sp_Arso1>(dictParams, "sp_cms_report_debt_detail").ToList();
      var dataList = Mapper.Map<List<sp_Arso1>, List<ReportDebtDetail>>(list);
      return dataList;
    }
  }
}
