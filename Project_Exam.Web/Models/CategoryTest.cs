using Entity;

namespace Project_Exam.Web.Models
{
    public class CategoryTest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
