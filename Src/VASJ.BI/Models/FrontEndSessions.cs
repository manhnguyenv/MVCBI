using System.Web;

namespace VASJ.BI.Models
{
    public static class FrontEndSessions
    {
        public static Account CurrentAccount
        {
            get
            {
                if (HttpContext.Current.Session["FrontEnd_CurrentAccount"] != null)
                    return HttpContext.Current.Session["FrontEnd_CurrentAccount"] as Account;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["FrontEnd_CurrentAccount"] = value;
            }
        }
    }
}