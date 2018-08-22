using System;
using System.Web;
using System.Web.Mvc;

namespace BI.Filters
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public sealed class PersistQuerystringAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      string SessionKey = "FILTER_" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;

      HttpContext context = HttpContext.Current;

      if (context.Request["reset"].ConvertTo<string>(string.Empty, true) == "true")
      {
        context.Session.Remove(SessionKey);
        filterContext.Result = new RedirectResult(context.Request.Url.AbsolutePath);
      }
      else
      {
        if (context.Request.QueryString.Count > 0)
        {
          context.Session[SessionKey] = context.Request.RawUrl;
        }
        else
        {
          if (context.Session[SessionKey] != null)
          {
            filterContext.Result = new RedirectResult(context.Session[SessionKey].ToString());
          }
        }
      }
    }
  }
}
