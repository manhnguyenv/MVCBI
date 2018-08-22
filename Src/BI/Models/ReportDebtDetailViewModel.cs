using System.Collections.Generic;

namespace BI.Models
{
  public class ReportDebtDetailViewModel
  {
    public List<ReportDebtDetail> ListDebtDetail { get; set; }
    public List<ReportDebtDetailSummary> ListDebtDetailSummary { get; set; }

    public ReportDebtDetailViewModel()
    {
      ListDebtDetail = new List<ReportDebtDetail>();
      ListDebtDetailSummary = new List<ReportDebtDetailSummary>();
    }
  }
}
