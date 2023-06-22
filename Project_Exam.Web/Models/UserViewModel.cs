using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Web.Models
{
    public class UserViewModel
    {
       [Required(ErrorMessage = "Ad gerekli")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Email adresiniz doğru formatta değil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string SelectedAppRoleId { get; set; }
        public List<SelectListItem> App { get; set; } = new();
        public string Id { get; set; }
    }
}
