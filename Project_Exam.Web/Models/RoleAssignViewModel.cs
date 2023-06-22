using Microsoft.AspNetCore.Mvc;

namespace Project_Exam.Web.Models
{
    public class RoleAssignViewModel 
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleAssign { get; set; }
    }
}
