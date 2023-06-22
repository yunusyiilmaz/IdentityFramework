using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Project_Exam.Models
{
    public class AddCategoryViewModel
    {
        [Required (ErrorMessage ="Kategori ismi giriniz")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
        public string SelectedCategoryId { get; set; }
    }
}
