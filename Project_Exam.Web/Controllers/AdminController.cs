using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Project_Exam.Models;
using Project_Exam.Web.Api;
using Project_Exam.Web.Hubs;
using Project_Exam.Web.Models;
using System.Net;

namespace Project_Exam.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly MyHub _myHub;

       
        public RoleManager<AppRole> _roleManager { get; set; }
        public UserManager<AppUser> _userManager { get; set; }
        public SignInManager<AppUser> _signInManager { get; set; }
        private readonly IHubContext<MyHub, IMyHub> _hubContext;
        public AdminController(RoleManager<AppRole> roleManager ,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, MyHub myHub, IHubContext<MyHub, IMyHub> hubContext) : base(userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _myHub = myHub;
            _hubContext = hubContext;          
        }


        public async Task<IActionResult> Index()
        {
            List<AppRole> roles = _roleManager.Roles.ToList();
            List<RoleViewModel> model = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                RoleViewModel rol = new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,

                };
                model.Add(rol);
            }
            return View(model);
        }

        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            List<RoleViewModel> model = new List<RoleViewModel>();
            foreach (var user in users)
            {
                RoleViewModel User = new RoleViewModel()
                {
                    Id = user.Id,
                    Name = user.UserName
                };
                model.Add(User);
            }
            return View(model);
        }


        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            AppRole Role = new AppRole();
            Role.Name = roleViewModel.Name;
            IdentityResult result = await _roleManager.CreateAsync(Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = _roleManager.DeleteAsync(role).Result;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            RoleViewModel model = new RoleViewModel();
            model.Id = role.Id;
            model.Name = role.Name;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleViewModel roleViewModel, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = roleViewModel.Name;
            IdentityResult result = _roleManager.UpdateAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(string id)
        {
            TempData["userId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            var userRole = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();
            List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();
            foreach (var role in roles)
            {

                RoleAssignViewModel roleAssignViewModel = new RoleAssignViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name

                };
                if (userRole.Contains(role.Name))
                {
                    roleAssignViewModel.RoleAssign = true;
                }
                else
                {
                    roleAssignViewModel.RoleAssign = false;
                }
                model.Add(roleAssignViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> roleAssignViewModels)
        {
            AppUser user = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
            foreach (var item in roleAssignViewModels)
            {
                if (item.RoleAssign)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == CurrentUser)
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
                return RedirectToAction("SignIn", "Login");
            }

            await _userManager.DeleteAsync(user);

            await _hubContext.Clients.All.DeleteUser();

            return RedirectToAction("UserList");

        }

        public IActionResult CreateUser()
        {
            var role = _roleManager.Roles.ToList();
            var model = new UserViewModel();
            model.App = role.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    var role = _roleManager.Roles.ToList();
            //    var model = new UserViewModel();
            //    model.App = role.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            //    return View(model);
            //}

            var user = new AppUser()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                PasswordHash = userViewModel.Password
            };

            var resultUser = await _userManager.CreateAsync(user, userViewModel.Password);
            var userRole = _roleManager.FindByIdAsync(userViewModel.SelectedAppRoleId);
            var resultUserRole = await _userManager.AddToRoleAsync(user, userRole.Result.Name);

            if (resultUserRole.Succeeded && resultUser.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                foreach (var item in resultUserRole.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                var role = _roleManager.Roles.ToList();
                var model = new UserViewModel();
                model.App = role.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
                return View(model);
            }

        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = _roleManager.Roles.ToList();
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            var model = new UserViewModel();
            model.Id = id;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.App = role.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Name == userRole }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;

            await _userManager.RemoveFromRoleAsync(user, userRole);

            var role = await _roleManager.FindByIdAsync(userViewModel.SelectedAppRoleId);
            IdentityResult result = _userManager.AddToRoleAsync(user, role.Name).Result;

            if (result.Succeeded)
            {
                await _hubContext.Clients.All.ChangeRole(userViewModel.Id, userViewModel.SelectedAppRoleId);
                return RedirectToAction("UserList");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            var roles = _roleManager.Roles.ToList();
            var model = new UserViewModel();
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.App = roles.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Name == userRole }).ToList();
            return View(model);

        }
    }
}

