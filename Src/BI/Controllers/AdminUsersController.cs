using BI.Filters;
using BI.Helpers;
using BI.Models;
using BI.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace BI.Controllers
{
  public partial class AdminController : AdminBaseController
  {
    //  /Admin/Users/
    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult Users(BackEndUsersList backEndUsersList)
    {
      Users users = new Users();
      backEndUsersList.UserList = users.GetAllUsers(backEndUsersList.Username, backEndUsersList.FullName, backEndUsersList.Email, backEndUsersList.GroupId);
      if (backEndUsersList.UserList.IsNull() || backEndUsersList.UserList.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }

      return View(backEndUsersList);
    }

    //  /Admin/UsersAdd/
    [HttpGet]
    [IsRestricted]
    public ActionResult UsersAdd()
    {
      var model = new BackEndUsersAdd();
      return View(model);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult UsersAdd(BackEndUsersAdd backEndUsersAdd)
    {
      if (ModelState.IsValidOrRefresh())
      {
        Users users = new Users();
        int? result = users.Add(backEndUsersAdd.Username, backEndUsersAdd.Password, backEndUsersAdd.FullName, backEndUsersAdd.Email, backEndUsersAdd.GroupId);
        switch (result)
        {
          case 0:
            ModelState.Clear();
            GrantUserDefaultPermissionsOnGroup(backEndUsersAdd.Username, backEndUsersAdd.GroupId);
            AddUserAndTenant(backEndUsersAdd.Username, backEndUsersAdd.IsTenant);
            backEndUsersAdd = new BackEndUsersAdd();
            ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.ItemSuccessfullyAdded);
            break;

          case 2:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UsernameAlreadyExists);
            break;

          default:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UnexpectedError);
            break;
        }
      }

      return View(backEndUsersAdd);
    }

    private void AddUserAndTenant(string username, bool isTenant)
    {
      try
      {
        var sb = new StringBuilder();
        if (isTenant)
        {
          sb.Append($"INSERT INTO [dbo].[Tenants]([Name],[MST],[UserName]) VALUES ({username}, {username}, {username})");
        }
        using (var con = ConnectionFactory.GetInstance())
        {
          string processQuery = sb.ToString();
          con.Open();
          con.Execute(processQuery);
        }
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
      }
    }

    private void GrantUserDefaultPermissionsOnGroup(string username, int? groupId)
    {
      try
      {
        var sb = new StringBuilder();
        var permissionIds = new List<int> { 9, 12, 53, 1140 };
        var permissions = new List<string> {
                    "add",
                    "browse",
                    "delete",
                    "edit",
                    "read"
                };

        // Grant permission to the following pages:
        //     (9)          /Admin/Logout
        //     (12)         /Admin/ChangePassword
        //     (53)         /Admin/Login
        //     (1140)       /Admin/ClearCache

        var browsePermission = permissions[1];
        for (int i = 0; i < permissionIds.Count; i++)
        {
          var pageId = permissionIds[i];
          sb.AppendLine($"INSERT INTO [dbo].[tb_admin_groups_pages_permissions]([GroupId],[PageId],[PermissionCode]) VALUES ({ groupId }, { pageId }, { browsePermission })");
        }

        using (var con = ConnectionFactory.GetInstance())
        {
          string processQuery = sb.ToString();
          con.Open();
          con.Execute(processQuery);
        }
      }
      catch (Exception ex)
      {
        MyLogger.Log.Error(ex.ToString());
      }
    }

    //  /Admin/UsersEdit/
    [HttpGet]
    [IsRestricted]
    public ActionResult UsersEdit(string id)
    {
      BackEndUsersEdit backEndUsersEdit = new BackEndUsersEdit();

      Users users = new Users();
      User user = users.GetUserByUserName(id);
      if (user.IsNotNull())
      {
        backEndUsersEdit.Username = user.UserName;
        backEndUsersEdit.FullName = user.FullName;
        backEndUsersEdit.Email = user.Email;
        backEndUsersEdit.GroupId = user.GroupId;
      }
      else
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemDoesNotExist);
        ViewData.IsFormVisible(false);
      }

      return View(backEndUsersEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult UsersEdit(BackEndUsersEdit backEndUsersEdit, string id)
    {
      Users users = new Users();
      int? result = users.Edit(id, backEndUsersEdit.Password, backEndUsersEdit.FullName, backEndUsersEdit.Email, backEndUsersEdit.GroupId);
      switch (result)
      {
        case 0:

          ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.ItemSuccessfullyEdited);
          break;

        case 2:
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemDoesNotExist);
          ViewData.IsFormVisible(false);
          break;

        default:
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UnexpectedError);
          break;
      }

      return View(backEndUsersEdit);
    }

    //  /Admin/UsersDelete/
    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    [ExportModelStateToTempData]
    public ActionResult UsersDelete(string deleteId)
    {
      Users users = new Users();
      switch (users.Delete(deleteId))
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

      return RedirectToAction("Users");
    }
  }
}
