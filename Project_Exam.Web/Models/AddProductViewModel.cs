using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Models
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Ürün adı yazınız")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ürün fiyatı yazınız")]   
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stok miktarını yazınız")]
        public int UnitQuantity { get; set; }
        public string SelectedCategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
        public Guid Id { get; set; }

    }
}
