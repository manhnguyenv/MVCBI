using BI.Filters;
using BI.Helpers;
using BI.Models;
using BI.ViewModels;
using Dapper;
using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace BI.Controllers
{
  public partial class AdminController : AdminBaseController
  {
    //  /Admin/Index
    [IsRestricted]
    public ActionResult Index()
    {
      return RedirectToAction("Dashboard");
    }

    // /Admin/ClearCache/
    [IsRestricted]
    public ActionResult ClearCache()
    {
      HttpRuntime.UnloadAppDomain();

      return RedirectToAction("Dashboard");
    }

    // /Admin/Uninstall/
    [IsRestricted]
    public ActionResult UninstallCms()
    {
      Configuration webConfigConfiguration = WebConfigurationManager.OpenWebConfiguration("~");
      webConfigConfiguration.ConnectionStrings.ConnectionStrings.Remove("MainConnectionString");
      webConfigConfiguration.Save();

      return Redirect("~/");
    }

    //  /Admin/Login/
    [HttpGet]
    public ActionResult Login(string ReturnUrl)
    {
      AdminPage backEndPage = new AdminPage();
      backEndPage.PageName = "Login";
      ViewBag.AdminPage = backEndPage;
      BackEndLogin backEndLogin = new BackEndLogin()
      {
        ReturnUrl = ReturnUrl
      };
      return View(backEndLogin);
    }

    //  /Admin/Login/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(BackEndLogin backEndLogin, string ReturnUrl)
    {
      AdminPage backEndPage = new AdminPage();
      backEndPage.PageName = "Login";
      ViewBag.AdminPage = backEndPage;
      if (ModelState.IsValidOrRefresh())
      {
        Users users = new Users();
        User user = users.GetUserByUserNameAndPassword(backEndLogin.Username, backEndLogin.Password);
        if (user.IsNotNull())
        {
          ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.SuccessfullyLoggedIn);
          var tenant = users.GetTenantByUsername(backEndLogin.Username);
          if (tenant != null)
          {
            // 1. Tạo người dùng (User) - UserName, Password
            // 2. Quản lý công ty -> Tạo công ty (Tenant) - Tên, MST, ServerName, DbBName, DbUserName, DbPassword, UserName
            // 3. Quản lý công ty -> Cập nhật lại thông tin UserName cho công ty (Tenant) nếu Tenant chưa có UserName
            //
            // Mỗi 1 công ty (Tenant) có duy nhất 1 mã số thuế (trùng với tên đăng nhập vào hệ thống)
            // Mỗi 1 công ty (Tenant) có duy nhất 1 connection string = { ServerName, DbBName, DbUserName, DbPassword, DbPort (optional) }
            // Mỗi 1 công ty (Tenant) có nhiều Đơn Vị Cơ Sở (dvcs)
            // Khi Login xong thì connection string sẽ thay đổi theo mã số thuế { ServerName, DbBName, DbUserName, DbPassword, DbPort (optional) }
            AdoHelper2.ConnectionString = DataHelper.BuildDynamicConnectionString(ConfigurationManager.ConnectionStrings["SM17ConnectionString"].ConnectionString, tenant);
            user.Dvcs = tenant.Dvcs;
            user.Tenant = tenant;
          }
          BackEndSessions.CurrentUser = user;
          AdminPages backEndPages = new AdminPages();
          BackEndSessions.CurrentMenu = backEndPages.GetMenuByGroupId(user.GroupId);
          if (ReturnUrl.IsNotEmptyOrWhiteSpace())
          {
            return Redirect(HttpUtility.UrlDecode(ReturnUrl));
          }
          else
          {
            return RedirectToAction("Dashboard");
          }
        }
        else
        {
          ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UsernameOrPasswordNotValid);
        }
      }

      return View(backEndLogin);
    }

    //  /Admin/Logout/
    [IsRestricted]
    public ActionResult Logout()
    {
      Session.Abandon();

      return RedirectToAction("Dashboard");
    }

    //  /Admin/ChangePassword/
    [HttpGet]
    [IsRestricted]
    public ActionResult ChangePassword()
    {
      return View();
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult ChangePassword(BackEndChangePassword backEndChangePassword)
    {
      if (ModelState.IsValidOrRefresh())
      {
        Users users = new Users();
        int? result = users.ChangePassword(BackEndSessions.CurrentUser.UserName, BackEndSessions.CurrentUser.Salt, backEndChangePassword.CurrentPassword, backEndChangePassword.NewPassword);
        switch (result)
        {
          case 0:
            ModelState.AddResult(ViewData, ModelStateResult.Success, Resources.Strings.PasswordSuccessfullyChanged);
            break;

          case 2:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.CurrentPasswordNotValid);
            break;

          default:
            ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.UnexpectedError);
            break;
        }
      }

      return View(backEndChangePassword);
    }

    //  /Admin/GetCmsModules/
    public ActionResult GetCmsModules()
    {
      return Json(ExtensionsHelper.GetCmsModules(false), JsonRequestBehavior.AllowGet);
    }

    //  /Admin/GetContentTemplates/
    public ActionResult GetContentTemplates()
    {
      ContentTemplates contentTemplates = new ContentTemplates();
      return Json(contentTemplates.GetAllContentTemplates(isActive: true), JsonRequestBehavior.AllowGet);
    }

    //  /Admin/IsSessionActive/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult IsSessionActive()
    {
      return Content(BackEndSessions.CurrentUser.IsNotNull().ToString(), "text/plain");
    }

    //  /Admin/IsPageBrowseAuthorized/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult IsPageBrowseAuthorized(string id)
    {
      AdminPages backEndPages = new AdminPages();
      AdminPage backEndPage = backEndPages.GetPageByAction(id);
      return Content(backEndPages.IsPermissionGranted(backEndPage.PageId, PermissionCode.Browse).ToString(), "text/plain");
    }

    //  /Admin/GetUniqueKey/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult GetUniqueKey()
    {
      return Content(DateTime.Now.Ticks.ToBase36(), "text/plain");
    }

    //  /Admin/GlobalConfiguration/
    [HttpGet]
    [IsRestricted]
    public ActionResult GlobalConfiguration()
    {
      BackEndGlobalConfigurationEdit backEndGlobalConfigurationEdit = new BackEndGlobalConfigurationEdit();

      GlobalConfigurations backEndGlobalConfigurations = new GlobalConfigurations();
      GlobalConfiguration globalConfiguration = backEndGlobalConfigurations.GetGlobalConfiguration();
      if (globalConfiguration.IsNotNull())
      {
        backEndGlobalConfigurationEdit.SiteName = globalConfiguration.SiteName;
        backEndGlobalConfigurationEdit.MetaTitle = globalConfiguration.MetaTitle;
        backEndGlobalConfigurationEdit.MetaKeywords = globalConfiguration.MetaKeywords;
        backEndGlobalConfigurationEdit.MetaDescription = globalConfiguration.MetaDescription;
        backEndGlobalConfigurationEdit.Robots = globalConfiguration.Robots;
        backEndGlobalConfigurationEdit.NotificationEmail = globalConfiguration.NotificationEmail;
        backEndGlobalConfigurationEdit.IsCanonicalizeActive = globalConfiguration.IsCanonicalizeActive;
        backEndGlobalConfigurationEdit.HostNameLabel = globalConfiguration.HostNameLabel;
        backEndGlobalConfigurationEdit.DomainName = globalConfiguration.DomainName;
        backEndGlobalConfigurationEdit.BingVerificationCode = globalConfiguration.BingVerificationCode;
        backEndGlobalConfigurationEdit.GoogleVerificationCode = globalConfiguration.GoogleVerificationCode;
        backEndGlobalConfigurationEdit.GoogleAnalyticsTrackingCode = globalConfiguration.GoogleAnalyticsTrackingCode;
        backEndGlobalConfigurationEdit.GoogleSearchCode = globalConfiguration.GoogleSearchCode;
        backEndGlobalConfigurationEdit.IsOffline = globalConfiguration.IsOffline;
        backEndGlobalConfigurationEdit.OfflineCode = globalConfiguration.OfflineCode;
        backEndGlobalConfigurationEdit.ServerTimeZone = globalConfiguration.ServerTimeZone;
        backEndGlobalConfigurationEdit.DateFormat = globalConfiguration.DateFormat;
        backEndGlobalConfigurationEdit.TimeFormat = globalConfiguration.TimeFormat;
        backEndGlobalConfigurationEdit.DefaultLanguageCode = globalConfiguration.DefaultLanguageCode;
        backEndGlobalConfigurationEdit.DefaultErrorPageTemplateId = globalConfiguration.DefaultErrorPageTemplateId;
        backEndGlobalConfigurationEdit.WhiteList = globalConfiguration.WhiteList;
      }
      else
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.ItemDoesNotExist);
        ViewData.IsFormVisible(false);
      }

      return View(backEndGlobalConfigurationEdit);
    }

    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult GlobalConfiguration(BackEndGlobalConfigurationEdit backEndGlobalConfigurationEdit)
    {
      GlobalConfigurations backEndGlobalConfigurations = new GlobalConfigurations();
      GlobalConfiguration globalConfiguration = backEndGlobalConfigurations.GetGlobalConfiguration();
      int? result = backEndGlobalConfigurations.Edit(
          backEndGlobalConfigurationEdit.SiteName,
          backEndGlobalConfigurationEdit.MetaTitle,
          backEndGlobalConfigurationEdit.MetaKeywords,
          backEndGlobalConfigurationEdit.MetaDescription,
          backEndGlobalConfigurationEdit.Robots,
          backEndGlobalConfigurationEdit.NotificationEmail,
          backEndGlobalConfigurationEdit.IsCanonicalizeActive,
          backEndGlobalConfigurationEdit.HostNameLabel,
          backEndGlobalConfigurationEdit.DomainName,
          backEndGlobalConfigurationEdit.BingVerificationCode,
          backEndGlobalConfigurationEdit.GoogleVerificationCode,
          backEndGlobalConfigurationEdit.GoogleAnalyticsTrackingCode,
          backEndGlobalConfigurationEdit.GoogleSearchCode,
          backEndGlobalConfigurationEdit.IsOffline,
          backEndGlobalConfigurationEdit.OfflineCode,
          backEndGlobalConfigurationEdit.ServerTimeZone,
          backEndGlobalConfigurationEdit.DateFormat,
          backEndGlobalConfigurationEdit.TimeFormat,
          backEndGlobalConfigurationEdit.DefaultLanguageCode,
          backEndGlobalConfigurationEdit.DefaultErrorPageTemplateId,
          backEndGlobalConfigurationEdit.WhiteList);
      switch (result)
      {
        case 0:
          if (globalConfiguration.HostNameLabel != backEndGlobalConfigurationEdit.HostNameLabel)
          {
            //Updates RouteTable
            using (RouteTable.Routes.GetWriteLock())
            {
              RouteTable.Routes.Clear();
              RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
          }

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

      return View(backEndGlobalConfigurationEdit);
    }

    //  /Admin/ErrorPage/
    public ActionResult ErrorPage(string errorPage, string errorMessage)
    {
      AdminPage backEndPage = new AdminPage();
      backEndPage.PageName = Resources.Strings.ErrorOccurred;
      ViewBag.AdminPage = backEndPage;

      ViewBag.ErrorPage = errorPage;
      ViewBag.ErrorMessage = errorMessage;

      return View();
    }

    //  /Admin/DeleteDatas
    [HttpGet]
    [IsRestricted]
    public ActionResult DeleteDatas()
    {
      return View();
    }

    //  /Admin/DeleteSubmit
    [HttpGet]
    [IsRestricted]
    public ActionResult DeleteSubmit()
    {
      try
      {
        var sb = new StringBuilder();

        sb.AppendLine("delete LogSystem");
        sb.AppendLine("delete TransactionLog");

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

      return View();
    }

    //  /Admin/Welcome
    [HttpGet]
    [IsRestricted]
    public ActionResult Welcome()
    {
      return View();
    }

    //  /Admin/Tenants
    [HttpGet]
    [IsRestricted]
    [PersistQuerystring]
    [ImportModelStateFromTempData]
    public ActionResult Tenants(BackEndTenantList backEndTenantList)
    {
      Tenants tenants = new Tenants();
      backEndTenantList.Tenants = tenants.GetAll();
      if (backEndTenantList.Tenants.IsNull() || backEndTenantList.Tenants.Count == 0)
      {
        ModelState.AddResult(ViewData, ModelStateResult.Error, Resources.Strings.NoDataFound);
      }
      return View(backEndTenantList);
    }

    //  /Admin/TenantAddEdit
    [HttpGet]
    [IsRestricted]
    public ActionResult TenantAddEdit(int? id)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      BackEndTenantAddEdit tenantAddEdit = new BackEndTenantAddEdit();
      if (id.IsNotNull())
      {
        Tenants tenants = new Tenants();
        var tenant = tenants.FilterById(id);
        if (tenant.IsNotNull())
        {
          tenant.UserName = tenant.MST; //Important

          tenantAddEdit.Id = tenant.Id;
          tenantAddEdit.Name = tenant.Name;
          tenantAddEdit.NameEn = tenant.NameEn;
          tenantAddEdit.MST = tenant.MST;
          tenantAddEdit.Dvcs = tenant.Dvcs;
          tenantAddEdit.Email = tenant.Email;
          tenantAddEdit.Phone = tenant.Phone;
          tenantAddEdit.UserName = tenant.UserName;
          tenantAddEdit.Representative = tenant.Representative;
          tenantAddEdit.Domain = tenant.Domain;
          tenantAddEdit.Address = tenant.Address;
          tenantAddEdit.ServerName = tenant.ServerName;
          tenantAddEdit.DbName = tenant.DbName;
          tenantAddEdit.DbUserName = tenant.DbUserName;
          tenantAddEdit.DbPassword = SecurityHelper.Decrypt(tenant.DbPassword);
          tenantAddEdit.DbPort = tenant.DbPort;
          tenantAddEdit.DateIssue = tenant.DateIssue.ToDateTimeString();
          tenantAddEdit.DateActive = tenant.DateActive.ToDateTimeString();
        }
      }
      var allItems_DmDvcs = RefDataHelper.GetAllItems_DmDvcs();
      if (allItems_DmDvcs != null && allItems_DmDvcs.Count > 0)
      {
        tenantAddEdit.DmDvcss = allItems_DmDvcs;
      }
      tenantAddEdit.ShowHideDbName = (username == "Administrator"); // Ẩn hiện để sửa Connection String
      return View(tenantAddEdit);
    }

    //  /Admin/TenantAddEdit
    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    public ActionResult TenantAddEdit(BackEndTenantAddEdit tenantAddEdit)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      Tenants tenants = new Tenants();
      int? currentId = tenantAddEdit.Id;
      var tenant = tenants.FilterById(currentId);
      if (tenant.IsNotNull())
      {
        tenant.UserName = tenant.MST; //Important

        tenantAddEdit.Id = tenant.Id;
        tenantAddEdit.MST = tenant.MST;
        tenantAddEdit.UserName = tenant.UserName;
      }
      if (ModelState.IsValidOrRefresh())
      {
        var rs = tenants.AddEdit(
            currentId,
            tenantAddEdit.MST,
            tenantAddEdit.Dvcs,
            tenantAddEdit.Name,
            tenantAddEdit.NameEn,
            tenantAddEdit.Email,
            tenantAddEdit.Phone,
            tenantAddEdit.Representative,
            tenantAddEdit.Domain,
            tenantAddEdit.Address,
            tenantAddEdit.ServerName,
            tenantAddEdit.DbName,
            tenantAddEdit.DbUserName,
            SecurityHelper.Encrypt(tenantAddEdit.DbPassword),
            tenantAddEdit.DbPort,
            tenantAddEdit.DateIssue.ToDateTime(),
            tenantAddEdit.DateActive.ToDateTime(),
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
      return View(tenantAddEdit);
    }

    //  /Admin/TenantDelete
    [HttpPost]
    [IsRestricted]
    [ValidateAntiForgeryToken]
    [ExportModelStateToTempData]
    public ActionResult TenantDelete(int? deleteId)
    {
      string username = BackEndSessions.CurrentUser.UserName;
      Tenants tenants = new Tenants();
      switch (tenants.Delete(deleteId, username))
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
      return RedirectToAction("Tenants");
    }
  }
}
