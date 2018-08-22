using BI.Filters;
using BI.Helpers;
using BI.Models;
using BI.ViewModels;
using System.Web.Mvc;

namespace BI.Controllers
{
  public partial class AdminController : AdminBaseController
  {
    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult TransactionLogs(BackEndTransactionLogList backEndTransactionLogList)
    {
      TransactionLogs transactionLogs = new TransactionLogs();
      backEndTransactionLogList.TransactionLogs = transactionLogs.GetAll(
              backEndTransactionLogList.TransationDate,
              backEndTransactionLogList.UserId,
              backEndTransactionLogList.Nick,
              backEndTransactionLogList.Reason
          );
      if (backEndTransactionLogList.TransactionLogs.IsNull() || backEndTransactionLogList.TransactionLogs.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndTransactionLogList);
    }

    [HttpGet]
    [IsRestricted]
    public ActionResult TransactionLogAddEdit(int? id)
    {
      BackEndTransactionLogAddEdit transactionLogAddEdit = new BackEndTransactionLogAddEdit();
      if (id.IsNotNull())
      {
        TransactionLogs transactionLogs = new TransactionLogs();
        var transactionLog = transactionLogs.FilterById(id);
        if (transactionLog.IsNotNull())
        {
          transactionLogAddEdit.Id = transactionLog.Id;
          transactionLogAddEdit.Id_Fighting = transactionLog.Id_Fighting;
          transactionLogAddEdit.TransationDate = transactionLog.TransationDate.ToDateTimeString();
          transactionLogAddEdit.ReceiveAccount = transactionLog.ReceiveAccount;
          transactionLogAddEdit.TransationType = transactionLog.TransationType;
          transactionLogAddEdit.Amount = transactionLog.Amount;
          transactionLogAddEdit.UserId = transactionLog.UserId;
          transactionLogAddEdit.Nick = transactionLog.Nick;
          transactionLogAddEdit.Reason = transactionLog.Reason;
          transactionLogAddEdit.Status = transactionLog.Status;
          transactionLogAddEdit.ErrorMessage = transactionLog.ErrorMessage;
          transactionLogAddEdit.Created_At = transactionLog.Created_At.ToDateTimeString();
          transactionLogAddEdit.Updated_At = transactionLog.Updated_At.ToDateTimeString();
        }
      }
      return View(transactionLogAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult TransactionLogAddEdit(BackEndTransactionLogAddEdit transactionLogAddEdit)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      TransactionLogs categories = new TransactionLogs();
      long? currentId = transactionLogAddEdit.Id;
      if (ModelState.IsValidOrRefresh())
      {
        var rs = categories.AddEdit(
            currentId,
            transactionLogAddEdit.Id_Fighting,
            transactionLogAddEdit.TransationDate.ToDateTime(),
            transactionLogAddEdit.ReceiveAccount,
            transactionLogAddEdit.TransationType,
            transactionLogAddEdit.Amount,
            transactionLogAddEdit.UserId,
            transactionLogAddEdit.Nick,
            transactionLogAddEdit.Reason,
            transactionLogAddEdit.Status,
            transactionLogAddEdit.ErrorMessage,
            transactionLogAddEdit.Created_At.ToDateTime(),
            transactionLogAddEdit.Updated_At.ToDateTime(),
            username
        );
        switch (rs)
        {
          case 0:
            ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.ItemSuccessfullyAddEdit);
            break;

          case 2:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemDoesNotExist);
            break;

          default:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UnexpectedError);
            break;
        }
      }
      return View(transactionLogAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    [ExportModelStateToTempData]
    public ActionResult TransactionLogDelete(int? deleteId)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      TransactionLogs transactionLogs = new TransactionLogs();
      switch (transactionLogs.Delete(deleteId, username))
      {
        case 0:
          ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.ItemSuccessfullyDeleted);
          break;

        case 2:
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemDoesNotExist);
          break;

        case 3:
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemUsedSomewhereElse);
          break;

        default:
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UnexpectedError);
          break;
      }
      return RedirectToAction("TransactionLogs");
    }
  }
}
