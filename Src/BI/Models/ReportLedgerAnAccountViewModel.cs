using System.Collections.Generic;

namespace BI.Models
{
  public class ReportLedgerAnAccountViewModel
  {
    public List<ReportLedgerAnAccount> ListLedgerAnAccount { get; set; }
    public List<ReportLedgerAnAccountSummary> ListLedgerAnAccountSummary { get; set; }

    public ReportLedgerAnAccountViewModel()
    {
      ListLedgerAnAccount = new List<ReportLedgerAnAccount>();
      ListLedgerAnAccountSummary = new List<ReportLedgerAnAccountSummary>();
    }
  }
}
