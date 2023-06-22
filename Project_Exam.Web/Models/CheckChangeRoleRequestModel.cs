namespace Project_Exam.Web.Models
{
    public class CheckChangeRoleRequestModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }


    public class CheckDeleteUserRequestModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class CheckAddProductRequestModel
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitQuantity { get; set; }
    }

}
