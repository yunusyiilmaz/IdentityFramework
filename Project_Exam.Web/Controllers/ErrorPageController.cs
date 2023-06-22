using Microsoft.AspNetCore.Mvc;

namespace Project_Exam.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        [Route("ErrorPage/{statuscode}")]
        public IActionResult Index(int statuscode)
        {
            switch(statuscode)
            {
                case 404:
                    ViewData["Error"] = "Page not found";
                    break;
                default:
                    break;
            }
            return View("ErrorPage");
        }
    }
}
