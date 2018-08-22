using System.Web;

namespace BI.Models
{
  public static class BackEndSessions
  {
    public static string CurrentMenu
    {
      get
      {
        if (HttpContext.Current.Session["BackEnd_CurrentMenu"] != null)
          return HttpContext.Current.Session["BackEnd_CurrentMenu"] as string;
        else
          return null;
      }
      set
      {
        HttpContext.Current.Session["BackEnd_CurrentMenu"] = value;
      }
    }

    public static User CurrentUser
    {
      get
      {
        if (HttpContext.Current.Session["BackEnd_CurrentUser"] != null)
          return HttpContext.Current.Session["BackEnd_CurrentUser"] as User;
        else
          return null;
      }
      set
      {
        HttpContext.Current.Session["BackEnd_CurrentUser"] = value;
      }
    }
  }
}
