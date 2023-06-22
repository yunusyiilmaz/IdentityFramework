using Core.Concrete;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }       
        public virtual List<Product> Products { get; set; }
       
    }
 
}
