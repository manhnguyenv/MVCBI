using System.Web.Mvc;
using VASJ.BI.Models;

namespace VASJ.BI.Controllers
{
    public class AdminBaseController : Controller
    {
        public AdminBaseController()
            : base()
        {
            //Constructor
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            AdminPages backEndPages = new AdminPages();
            AdminPage backEndPage = backEndPages.GetPageByAction(RouteData.GetRequiredString("action")) ?? new AdminPage();

            backEndPage.IsModal = requestContext.HttpContext.Request["IsModal"].ConvertTo<bool>(false, true);

            ViewBag.AdminPage = backEndPage;
        }
    }
}