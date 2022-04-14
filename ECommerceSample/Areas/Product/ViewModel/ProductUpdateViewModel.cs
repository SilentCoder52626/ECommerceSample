using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Areas.Product.ViewModel
{
    public class ProductUpdateViewModel
    {
        public long Id { get; set; }
        public string OldImage { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public long TagId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
