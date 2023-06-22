using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Project_Exam.Web.Models
{
    public class BaseController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;

        public BaseController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected AppUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;
    }
}
