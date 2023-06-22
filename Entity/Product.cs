using Core.Concrete;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Product : BaseEntity
    {
     
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UnitQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
