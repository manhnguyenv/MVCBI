using BI.Models;
using System.Web.Mvc;

namespace BI.Controllers
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
