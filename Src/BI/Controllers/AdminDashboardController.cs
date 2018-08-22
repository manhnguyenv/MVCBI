using BI.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BI.Controllers
{
  public partial class AdminController : AdminBaseController
  {
    // GET: /Admin/Dashboard
    [IsRestricted]
    public ActionResult Dashboard()
    {
      return View();
    }

    public JsonResult GetDataColumnChartByQuarter()
    {
      List<ColumnChartDataItem> ColumnChartData = new List<ColumnChartDataItem>();

      //Fake
      ColumnChartData.Add(new ColumnChartDataItem(1, 7, 455, 315, 500, 410));
      ColumnChartData.Add(new ColumnChartDataItem(1, 8, 120, 150, 100, 200));
      ColumnChartData.Add(new ColumnChartDataItem(1, 9, 120, 150, 100, 200));

      //Sort by Month
      ColumnChartData = ColumnChartData.OrderBy(a => a.Month).ToList();
      var chartData = new object[ColumnChartData.Count + 1];
      chartData[0] = new object[]{
                    "Month",
                    "SoL",
                    "TBan",
                    "TVon",
                    "LoiN"
                };
      int j = 0;
      foreach (var i in ColumnChartData)
      {
        j++;
        chartData[j] = new object[] { i.Month, i.SoL, i.TBan, i.TVon, i.LoiN };
      }

      return new JsonResult { Data = chartData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
    }
  }

  //DTO
  public class ColumnChartDataItem
  {
    public int ID { get; set; }
    public int? Month { get; set; }
    public int? SoL { get; set; }
    public int? TBan { get; set; }
    public int? TVon { get; set; }
    public int? LoiN { get; set; }

    public ColumnChartDataItem()
    {
    }

    public ColumnChartDataItem(int id, int month, int soLuong, int tienBan, int tienVon, int loiNhuan)
    {
      ID = id;
      Month = month;
      SoL = soLuong;
      TBan = tienBan;
      TVon = tienVon;
      LoiN = loiNhuan;
    }
  }
}
