using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Models
{
    public class ProductDetailViewModel
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        [Display(Name="Brand")]
        public string BrandName { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Tag")]
        public string TagName { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string AvailabilityStatus { get; set; }
    }
}
