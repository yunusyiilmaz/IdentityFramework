using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email alanı gereklidir")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Şifre gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmalıdır.")]
        public string? Password { get; set; }
    }
}
