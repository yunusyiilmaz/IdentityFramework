using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Project_Exam.Web.CustomValidator
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            string[] digits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            foreach(var digit in digits) 
            {               
                if (user.UserName[0].ToString()==digit) 
                {
                     errors.Add(new IdentityError() { Code = "UserNameContainsFirstLetterDigitsContains", Description = "Kullanıcı Adının ilk karakteri rakam ile başlayamaz" });
                }
                
            }
            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
