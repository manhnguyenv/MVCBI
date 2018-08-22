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
    public ActionResult LogSystems(BackEndLogSystemList backEndLogSystemList)
    {
      LogSystems logSystems = new LogSystems();
      backEndLogSystemList.LogSystems = logSystems.GetAll();
      if (backEndLogSystemList.LogSystems.IsNull() || backEndLogSystemList.LogSystems.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndLogSystemList);
    }

    [HttpGet]
    [IsRestricted]
    public ActionResult LogSystemAddEdit(int? id)
    {
      BackEndLogSystemAddEdit logSystemAddEdit = new BackEndLogSystemAddEdit();
      if (id.IsNotNull())
      {
        LogSystems logSystems = new LogSystems();
        var logSystem = logSystems.FilterById(id);
        if (logSystem.IsNotNull())
        {
          logSystemAddEdit.id = logSystem.id;
          logSystemAddEdit.user_id = logSystem.user_id;
          logSystemAddEdit.table_name = logSystem.table_name;
          logSystemAddEdit.record_id = logSystem.record_id;
          logSystemAddEdit.username = logSystem.username; // Admin account
          logSystemAddEdit.type = logSystem.type.GetValueOrDefault();
          logSystemAddEdit.description = logSystem.description;
          logSystemAddEdit.created_at = logSystem.created_at.ToDateTimeString();
          logSystemAddEdit.updated_at = logSystem.updated_at.ToDateTimeString();
          logSystemAddEdit.deleted_at = logSystem.deleted_at.ToDateTimeString();
        }
      }
      return View(logSystemAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult LogSystemAddEdit(BackEndLogSystemAddEdit logSystemAddEdit)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      LogSystems categories = new LogSystems();
      int? currentId = logSystemAddEdit.id;
      if (ModelState.IsValidOrRefresh())
      {
        var rs = categories.AddEdit(
            currentId,
            logSystemAddEdit.user_id,
            username, //TODO: Manh
            logSystemAddEdit.type,
            logSystemAddEdit.description,
            logSystemAddEdit.created_at.ToDateTime(),
            logSystemAddEdit.updated_at.ToDateTime(),
            logSystemAddEdit.deleted_at.ToDateTime()
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
      return View(logSystemAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    [ExportModelStateToTempData]
    public ActionResult LogSystemDelete(int? deleteId)
    {
      LogSystems logSystems = new LogSystems();
      switch (logSystems.Delete(deleteId))
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
      return RedirectToAction("LogSystems");
    }
  }
}
