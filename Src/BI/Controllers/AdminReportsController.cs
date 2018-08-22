using BI.Filters;
using BI.Helpers;
using BI.Models;
using BI.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace BI.Controllers
{
  public partial class AdminController : AdminBaseController
  {
    // GET: /Admin/ReportDebtDetails

    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult ReportDebtDetails(BackEndReportDebtDetailList backEndReportDebtDetailList)
    {
      ReportDebtDetailModel reportDebtDetailModel = new ReportDebtDetailModel();

      if (BackEndSessions.CurrentUser != null
          && !string.IsNullOrWhiteSpace(BackEndSessions.CurrentUser.Dvcs))
      {
        var allItems_DmTk = RefDataHelper.GetAllItems_DmTk(BackEndSessions.CurrentUser.Dvcs);
        if (allItems_DmTk != null && allItems_DmTk.Count > 0)
        {
          // Lọc chọn ra những tài khoản công nợ
          backEndReportDebtDetailList.DmTks = allItems_DmTk.Where(m => m.Tk_Cn == 1).OrderBy(m => m.Ten_Tk_UTF8).ToList();
        }

        var allItems_DmKh = RefDataHelper.GetAllItems_DmKh(BackEndSessions.CurrentUser.Dvcs);
        if (allItems_DmKh != null && allItems_DmKh.Count > 0)
        {
          backEndReportDebtDetailList.DmKhs = allItems_DmKh.OrderBy(m => m.Ten_Kh_UTF8).ToList();
        }
      }

      var allItems_DmDvcs = RefDataHelper.GetAllItems_DmDvcs();
      if (allItems_DmDvcs != null && allItems_DmDvcs.Count > 0)
      {
        backEndReportDebtDetailList.DmDvcss = allItems_DmDvcs;
      }

      // SET DATEFORMAT DMY;
      // EXEC sp_arso1 '131%','%','01/01/2018','30/06/2018','04'
      backEndReportDebtDetailList.ReportDebtDetails = reportDebtDetailModel.GetAll(
              string.Concat(backEndReportDebtDetailList.UserId, "%"),     //Using the SQL % Wildcard
              string.Concat(backEndReportDebtDetailList.CustomerId, "%"), //Using the SQL % Wildcard
              string.Concat(backEndReportDebtDetailList.MaDonVi, "%"),    //Using the SQL % Wildcard
              backEndReportDebtDetailList.StartDate,
              backEndReportDebtDetailList.EndDate,
              backEndReportDebtDetailList.ChiTietTheoHH,
              backEndReportDebtDetailList.MauBaoCao,
              backEndReportDebtDetailList.NgoaiTe
          );
      if (backEndReportDebtDetailList.IsNull() ||
          backEndReportDebtDetailList.ReportDebtDetails.IsNull() ||
          backEndReportDebtDetailList.ReportDebtDetails.ListDebtDetail.IsNull() ||
          backEndReportDebtDetailList.ReportDebtDetails.ListDebtDetail.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndReportDebtDetailList);
    }

    // GET: /Admin/ReportBalanceSheets

    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult ReportBalanceSheets(BackEndReportBalanceSheetList backEndReportBalanceSheetList)
    {
      ReportBalanceSheetModel reportBalanceSheetModel = new ReportBalanceSheetModel();

      if (BackEndSessions.CurrentUser != null
          && !string.IsNullOrWhiteSpace(BackEndSessions.CurrentUser.Dvcs))
      {
        var allItems_DmTk = RefDataHelper.GetAllItems_DmTk(BackEndSessions.CurrentUser.Dvcs);
        if (allItems_DmTk != null && allItems_DmTk.Count > 0)
        {
          // Lọc chọn ra những tài khoản công nợ
          backEndReportBalanceSheetList.DmTks = allItems_DmTk.Where(m => m.Tk_Cn == 1).OrderBy(m => m.Ten_Tk_UTF8).ToList();
        }

        var allItems_DmKh = RefDataHelper.GetAllItems_DmKh(BackEndSessions.CurrentUser.Dvcs);
        if (allItems_DmKh != null && allItems_DmKh.Count > 0)
        {
          backEndReportBalanceSheetList.DmKhs = allItems_DmKh.OrderBy(m => m.Ten_Kh_UTF8).ToList();
        }
      }

      var allItems_DmDvcs = RefDataHelper.GetAllItems_DmDvcs();
      if (allItems_DmDvcs != null && allItems_DmDvcs.Count > 0)
      {
        backEndReportBalanceSheetList.DmDvcss = allItems_DmDvcs;
      }

      // SET DATEFORMAT DMY;
      // EXEC sp_arcd1 '131%','%','01/01/2018','30/06/2018','04'
      backEndReportBalanceSheetList.ReportBalanceSheets = reportBalanceSheetModel.GetAll(
              string.Concat(backEndReportBalanceSheetList.UserId, "%"),     //Using the SQL % Wildcard
              string.Concat(backEndReportBalanceSheetList.CustomerId, "%"), //Using the SQL % Wildcard
              string.Concat(backEndReportBalanceSheetList.MaDonVi, "%"),    //Using the SQL % Wildcard
              backEndReportBalanceSheetList.StartDate,
              backEndReportBalanceSheetList.EndDate
          );

      if (backEndReportBalanceSheetList.ReportBalanceSheets.IsNull() || backEndReportBalanceSheetList.ReportBalanceSheets.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }

      return View(backEndReportBalanceSheetList);
    }

    // GET: /Admin/ReportLedgerAnAccounts

    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult ReportLedgerAnAccounts(BackEndReportLedgerAnAccountList backEndReportLedgerAnAccountList)
    {
      ReportLedgerAnAccountModel reportLedgerAnAccountModel = new ReportLedgerAnAccountModel();

      if (BackEndSessions.CurrentUser != null
          && !string.IsNullOrWhiteSpace(BackEndSessions.CurrentUser.Dvcs))
      {
        var allItems_DmTk = RefDataHelper.GetAllItems_DmTk(BackEndSessions.CurrentUser.Dvcs);
        if (allItems_DmTk != null && allItems_DmTk.Count > 0)
        {
          backEndReportLedgerAnAccountList.DmTks = allItems_DmTk.OrderBy(m => m.Ten_Tk_UTF8).ToList();
        }
      }

      var allItems_DmDvcs = RefDataHelper.GetAllItems_DmDvcs();
      if (allItems_DmDvcs != null && allItems_DmDvcs.Count > 0)
      {
        backEndReportLedgerAnAccountList.DmDvcss = allItems_DmDvcs;
      }

      // SET DATEFORMAT DMY;
      // EXEC usp_Nkc_SoCaiMotTaiKhoanBak
      //   @_DocDate1 = '01/01/2018',
      //   @_DocDate2 = '10/01/2018',
      //   @_Account = '111',
      //   @_BranchCode = '04',
      //   @_IsCurrency = 1

      //Correct data before execute stored procedure
      if (backEndReportLedgerAnAccountList.UserId.IsNotEmptyOrWhiteSpace())
      {
        backEndReportLedgerAnAccountList.UserId = backEndReportLedgerAnAccountList.UserId.Trim();
      }

      backEndReportLedgerAnAccountList.ReportLedgerAnAccounts = reportLedgerAnAccountModel.GetAll(
              backEndReportLedgerAnAccountList.UserId,
              backEndReportLedgerAnAccountList.MaDonVi,
              backEndReportLedgerAnAccountList.StartDate,
              backEndReportLedgerAnAccountList.EndDate,
              backEndReportLedgerAnAccountList.MauBaoCao,
              backEndReportLedgerAnAccountList.NgoaiTe
          );
      if (backEndReportLedgerAnAccountList.IsNull() ||
          backEndReportLedgerAnAccountList.ReportLedgerAnAccounts.IsNull() ||
          backEndReportLedgerAnAccountList.ReportLedgerAnAccounts.ListLedgerAnAccount.IsNull() ||
          backEndReportLedgerAnAccountList.ReportLedgerAnAccounts.ListLedgerAnAccount.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndReportLedgerAnAccountList);
    }

    // GET: /Admin/ReportBalanceSheetAccounts

    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult ReportBalanceSheetAccounts(BackEndReportBalanceSheetAccountList backEndReportBalanceSheetAccountList)
    {
      ReportBalanceSheetAccountModel reportBalanceSheetAccountModel = new ReportBalanceSheetAccountModel();

      var allItems_DmDvcs = RefDataHelper.GetAllItems_DmDvcs();
      if (allItems_DmDvcs != null && allItems_DmDvcs.Count > 0)
      {
        backEndReportBalanceSheetAccountList.DmDvcss = allItems_DmDvcs;
      }

      // SET DATEFORMAT DMY;
      // EXEC sp_glcd2 '01/01/2018','30/07/2018','04%'
      backEndReportBalanceSheetAccountList.ReportBalanceSheetAccounts = reportBalanceSheetAccountModel.GetAll(
                    string.Concat(backEndReportBalanceSheetAccountList.MaDonVi, "%"),    //Using the SQL % Wildcard
                    backEndReportBalanceSheetAccountList.StartDate,
                    backEndReportBalanceSheetAccountList.EndDate
                );

      if (backEndReportBalanceSheetAccountList.ReportBalanceSheetAccounts.IsNull() || backEndReportBalanceSheetAccountList.ReportBalanceSheetAccounts.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }

      return View(backEndReportBalanceSheetAccountList);
    }
  }
}
