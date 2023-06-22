using Entity;

namespace Project_Exam.Models
{
    public class ProductViewModel
    {
        public List<Product> products { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitQuantity { get; set; }

    }
 
}
