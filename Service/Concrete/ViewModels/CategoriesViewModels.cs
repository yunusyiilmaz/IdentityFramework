using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete.ViewModels
{
    public class CategoriesViewModels
    {
        public List<Category> Categories { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
