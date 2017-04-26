using System.Web.Mvc;

namespace Freelance.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        public ActionResult InternalServer()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}