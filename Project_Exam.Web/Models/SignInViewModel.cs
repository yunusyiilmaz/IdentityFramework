using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Ad gerekli")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Email adresiniz doğru formatta değil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserRole { get; set; }

        public string SelectedAppRoleId { get; set; }
        public List<SelectListItem> App { get; set; } = new();
    }
}
