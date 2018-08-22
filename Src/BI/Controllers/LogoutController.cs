using System.Web.Mvc;
using VASJ.BI.Models;

namespace VASJ.BI.Controllers
{
    public class LogoutController : Controller
    {
        // GET: /Logout/Index
        [HttpGet]
        public ActionResult Index()
        {
            FrontEndSessions.CurrentAccount = null;
            return RedirectToAction("Index", "Default");
        }
    }
}