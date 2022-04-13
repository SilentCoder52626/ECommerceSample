using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Areas.Product.ViewModel
{
    public class BrandCreateViewModel
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
    }
}
