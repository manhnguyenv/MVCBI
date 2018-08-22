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
    public ActionResult ConfigEvents(BackEndConfigEventList backEndConfigEventList)
    {
      ConfigEvents configEvents = new ConfigEvents();
      backEndConfigEventList.ConfigEvents = configEvents.GetAll();
      if (backEndConfigEventList.ConfigEvents.IsNull() || backEndConfigEventList.ConfigEvents.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndConfigEventList);
    }

    [HttpGet]
    [IsRestricted]
    public ActionResult ConfigEventAddEdit(int? id)
    {
      BackEndConfigEventAddEdit configEventAddEdit = new BackEndConfigEventAddEdit();
      if (id.IsNotNull())
      {
        ConfigEvents configEvents = new ConfigEvents();
        var configEvent = configEvents.FilterById(id);
        if (configEvent.IsNotNull())
        {
          configEventAddEdit.Id = configEvent.Id;
          configEventAddEdit.EventName = configEvent.EventName;
          configEventAddEdit.StartDate = configEvent.StartDate.ToDateTimeString();
          configEventAddEdit.EndDate = configEvent.EndDate.ToDateTimeString();
          configEventAddEdit.Status = configEvent.Status;
        }
      }
      return View(configEventAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult ConfigEventAddEdit(BackEndConfigEventAddEdit configEventAddEdit)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      ConfigEvents categories = new ConfigEvents();
      int? currentId = configEventAddEdit.Id;
      if (ModelState.IsValidOrRefresh())
      {
        var rs = categories.AddEdit(
            currentId,
            configEventAddEdit.EventName,
            configEventAddEdit.StartDate.ToDateTime(),
            configEventAddEdit.EndDate.ToDateTime(),
            configEventAddEdit.Status,
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
      return View(configEventAddEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    [ExportModelStateToTempData]
    public ActionResult ConfigEventDelete(int? deleteId)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      ConfigEvents configEvents = new ConfigEvents();
      switch (configEvents.Delete(deleteId, username))
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
      return RedirectToAction("ConfigEvents");
    }
  }
}
