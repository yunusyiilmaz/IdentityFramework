using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        //public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
