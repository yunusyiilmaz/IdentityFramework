using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project_Exam.Models;
using Project_Exam.Web.Api;
using Project_Exam.Web.Models;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using Tweetinvi.Core.Models;
using Tweetinvi.Models;
using static Tweetinvi.Parameters.V2.TweetResponseFields;

namespace Project_Exam.Controllers
{
    public class LoginController : Controller
    {
        private IMailService _mailService;
        public RoleManager<AppRole> RoleManager { get; set; }
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public LoginController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMailService mailService, SignInManager<AppUser> signInManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
            _mailService = mailService;
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {

                var userEmail = await UserManager.FindByEmailAsync(userLogin.Email);
               
                if (userEmail == null)
                {
                    ModelState.AddModelError("", "Kayıt Bulunamadı.");
                    return View();
                }
                if (UserManager.IsEmailConfirmedAsync(userEmail).Result == false)
                {
                    ModelState.AddModelError("", "Email adresiniz onaylanmamıştır. Lütfen  epostanızı kontrol ediniz.");
                    return View(userLogin);
                }
                if (await UserManager.IsLockedOutAsync(userEmail))
                {
                    ModelState.AddModelError("", "Hesabınız bir süre kitlenmiştir");
                }

                if (userEmail != null)
                {
                    await SignInManager.SignOutAsync();
                    var result = await SignInManager.PasswordSignInAsync(userEmail, userLogin.Password, false, false);
                    
                    if (result.Succeeded)
                    {
                        await UserManager.ResetAccessFailedCountAsync(userEmail);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await UserManager.AccessFailedAsync(userEmail);
                        var fail = await UserManager.GetAccessFailedCountAsync(userEmail);
                        if (fail == 3)
                        {
                            await UserManager.SetLockoutEndDateAsync(userEmail, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                            ModelState.AddModelError("", "Hesabınız 3 deneme sonrasında 10 dk süreliğine kitlenmiştir");
                        }
                    }
                    

                }
            }
            ModelState.AddModelError("", "Geçersiz email adresi veya şifresi");
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid) return View(signInViewModel);

            var user = new AppUser
            {
                UserName = signInViewModel.UserName,
                Email = signInViewModel.Email
            };

            var result = await UserManager.CreateAsync(user, signInViewModel.Password);
            var roles = RoleManager.Roles.ToList().Where(x => x.Name == "Member").FirstOrDefault();
            await UserManager.AddToRoleAsync(user, roles.ToString());

            if (result.Succeeded)
            {
                string confirmationEmailToken=await UserManager.GenerateEmailConfirmationTokenAsync(user);
                string link = Url.Action("ConfirmEmail", "Login", new
                {
                    userId = user.Id,
                    token = confirmationEmailToken
                }, protocol: HttpContext.Request.Scheme);
                Web.Helper.EmailConfirmation.SendEmail(link, user.Email);               
                return RedirectToAction("SignIn");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(signInViewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassWordViewModel resetPassWordViewModel)
        {
            var UserMail = UserManager.FindByEmailAsync(resetPassWordViewModel.Email).Result;
            if (UserMail != null)
            {
                string passwordResetToken = UserManager.GeneratePasswordResetTokenAsync(UserMail).Result;
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Login", new
                {
                    userId = UserMail.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme); 

            Project_Exam.Web.Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink, UserMail.Email);
                ViewBag.status = "success";
            }
            else
            {
                ModelState.AddModelError("", "Sistemde kayıtlı email adresi bulunamamıştır.");
            }

            return View(resetPassWordViewModel);
        }
        public async Task<IActionResult> ResetPasswordConfirm(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")] ResetPassWordViewModel resetPassWordViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

           AppUser user = await UserManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await UserManager.ResetPasswordAsync(user, token, resetPassWordViewModel.PasswordNew);

                if (result.Succeeded)
                {
                    await UserManager.UpdateSecurityStampAsync(user);

                    ViewBag.status = "success";
                }
                else
                {
                   foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "hata meydana gelmiştir. Lütfen daha sonra tekrar deneyiniz.");
            }

            return View(resetPassWordViewModel);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = UserManager.FindByIdAsync(userId);

            IdentityResult result =  UserManager.ConfirmEmailAsync(user.Result, token).Result;

            if (result.Succeeded)
            {
                ViewBag.status = "Email adresiniz onaylanmıştır. Login ekranından giriş yapabilirsiniz.";
            }
            else
            {
                ViewBag.status = "Bir hata meydana geldi. lütfen daha sonra tekrar deneyiniz.";
            }
            return View();
        }
        //public IActionResult FacebookLogin(string ReturnUrl)

        //{
        //    string RedirectUrl = Url.Action("ExternalResponse", "Login", new { ReturnUrl = ReturnUrl });

        //    var properties = SignInManager.ConfigureExternalAuthenticationProperties("Facebook", RedirectUrl);

        //    return new ChallengeResult("Facebook", properties);
        //}

        //public IActionResult GoogleLogin(string ReturnUrl)
        //{
        //    string RedirectUrl = Url.Action("ExternalResponse", "Login", new { ReturnUrl = ReturnUrl });

        //    var properties = SignInManager.ConfigureExternalAuthenticationProperties("Google", RedirectUrl);

        //    return new ChallengeResult("Google", properties);
        //}
        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        {
            ExternalLoginInfo info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("LogIn");
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                if (result.Succeeded)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    AppUser user = new AppUser();
                    user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
                    {
                        string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;

                        userName = userName.Replace(' ', '-').ToLower() + ExternalUserId.Substring(0, 5).ToString();

                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    }
                    AppUser user2 = await UserManager.FindByEmailAsync(user.Email);
                    if (user2 == null)
                    {
                        IdentityResult createResult = await UserManager.CreateAsync(user);
                        var roles = RoleManager.Roles.ToList().Where(x => x.Name == "Member").FirstOrDefault();
                        await UserManager.AddToRoleAsync(user, roles.ToString());
                        if (createResult.Succeeded)
                        {
                            IdentityResult loginResult = await UserManager.AddLoginAsync(user, info);
                            if (loginResult.Succeeded)
                            {
                                //     await signInManager.SignInAsync(user, true);
                                await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                AddModelError(loginResult);
                            }
                        }
                        else
                        {
                            AddModelError(createResult);
                        }
                    }
                    else
                    {
                        IdentityResult loginResult = await UserManager.AddLoginAsync(user2, info);
                        await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                        return Redirect(ReturnUrl);
                    }
                }
            }
            List<string> errors = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
            return View("Error", errors);
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}



