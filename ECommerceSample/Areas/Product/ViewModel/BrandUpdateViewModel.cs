using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Areas.Product.ViewModel
{
    public class BrandUpdateViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
