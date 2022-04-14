using ECommerce.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Areas.Product.ViewModel
{
    public class ProductCreateViewModel
    {
        public IList<Brand> Brands { get; set; }  = new List<Brand>();
        public SelectList BrandSelectList => new SelectList(Brands, nameof(Brand.BrandId), nameof(Brand.Name));
        public IList<Category> Categories { get; set; } = new List<Category>();
        public SelectList CategorySelectList => new SelectList(Categories, nameof(Category.CategoryId), nameof(Category.Name));

        public IList<Tag> Tags { get; set; } = new List<Tag>();
        public SelectList TagSelectList => new SelectList(Tags, nameof(Tag.TagId), nameof(Tag.Name));
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
        [Required]
        public string Color { get; set; }
    }
}
