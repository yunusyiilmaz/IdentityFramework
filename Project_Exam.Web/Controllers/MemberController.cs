using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Exam.Web.Models;

namespace Project_Exam.Web.Controllers;

public class MemberController : BaseController
{
    public MemberController(UserManager<AppUser> userManager) : base(userManager)
    {
    }

    public IActionResult AccsessDenied()
    {       
        return View();             
    }
}
