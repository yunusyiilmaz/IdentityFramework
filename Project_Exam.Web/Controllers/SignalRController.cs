using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Exam.Controllers;
using Project_Exam.Models;
using Project_Exam.Web.Models;

namespace Project_Exam.Web.Controllers
{

    public class SignalRController : BaseController
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SignalRController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager) : base(userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;

        }

        [HttpPost("check-change-role")]
        public async Task<bool> CheckForChangeRole([FromBody] CheckChangeRoleRequestModel request, string returnUrl)
        {
            if (CurrentUser.Id != request.UserId)
                return false;

            var user = CurrentUser;
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
            
            return true;
        }

        [HttpPost("check-delete-user")]
        public async Task<bool> CheckForDeleteUser()
        {
            try
            {
                if (CurrentUser is not null)
                    return false;

            }
            catch
            {
            }

            await _signInManager.SignOutAsync();
            return true;
        }
        /**************************************************************************************************/

        [HttpPost("check-RealTime")]
        public async Task<bool> CheckReaTime()
        {
            var user = CurrentUser;
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);

            return true;
        }
    }
}
