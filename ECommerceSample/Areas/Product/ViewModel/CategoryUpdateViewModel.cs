using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Areas.Product.ViewModel
{
    public class CategoryUpdateViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
