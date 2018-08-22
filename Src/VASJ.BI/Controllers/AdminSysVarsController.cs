using System.Web.Mvc;
using VASJ.BI.Filters;
using VASJ.BI.Models;
using VASJ.BI.ViewModels;

namespace VASJ.BI.Controllers
{
    public partial class AdminController : AdminBaseController
    {
        [HttpGet]
        [IsRestricted]
        [PersistQuerystring]
        [ImportModelStateFromTempData]
        public ActionResult SysVars(BackEndSysVarList backEndSysVarList)
        {
            SysVars sysVars = new SysVars();
            backEndSysVarList.SysVars = sysVars.GetAll();
            if (backEndSysVarList.SysVars.IsNull() || backEndSysVarList.SysVars.Count == 0)
            {
                ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
            }
            return View(backEndSysVarList);
        }

        [HttpGet]
        [IsRestricted]
        public ActionResult SysVarAddEdit(string id)
        {
            BackEndSysVarAddEdit sysVarAddEdit = new BackEndSysVarAddEdit();
            if (id.IsNotNull())
            {
                SysVars sysVars = new SysVars();
                var sysVar = sysVars.FilterById(id);
                if (sysVar.IsNotNull())
                {
                    sysVarAddEdit.Sys_Id = sysVar.Sys_Id;
                    sysVarAddEdit.Sys_Var = sysVar.Sys_Var;
                    sysVarAddEdit.Sys_Type = sysVar.Sys_Type;
                    sysVarAddEdit.Sys_Value = sysVar.Sys_Value;
                    sysVarAddEdit.Sys_Name = sysVar.Sys_Name;
                    sysVarAddEdit.IsEditable = sysVar.IsEditable;
                }
            }
            return View(sysVarAddEdit);
        }

        [HttpPost]
        [IsRestricted]
        [ValidateAntiForgeryToken]
        public ActionResult SysVarAddEdit(BackEndSysVarAddEdit sysVarAddEdit)
        {
            string username = BackEndSessions.CurrentUser.UserName;
            SysVars categories = new SysVars();
            string currentId = sysVarAddEdit.Sys_Id;
            if (ModelState.IsValidOrRefresh())
            {
                var rs = categories.AddEdit(
                    currentId,
                    sysVarAddEdit.Sys_Var,
                    sysVarAddEdit.Sys_Type,
                    sysVarAddEdit.Sys_Value,
                    sysVarAddEdit.Sys_Name,
                    sysVarAddEdit.IsEditable,
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
            return View(sysVarAddEdit);
        }

        [HttpPost]
        [IsRestricted]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult SysVarDelete(string deleteId)
        {
            string username = BackEndSessions.CurrentUser.UserName;
            SysVars sysVars = new SysVars();
            switch (sysVars.Delete(deleteId, username))
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
            return RedirectToAction("SysVars");
        }
    }
}