using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Web.Models
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Ad gerekli")]
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleAssign { get; set; }
    }
}
