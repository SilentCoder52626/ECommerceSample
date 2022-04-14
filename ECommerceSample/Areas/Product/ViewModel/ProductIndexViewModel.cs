namespace ECommerceSample.Areas.Product.ViewModel
{
    public class ProductIndexViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public long BrandId { get; set; }
        public string Brand { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
        public long TagId { get; set; }
        public string Tag { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string AvailibilityStatus { get; set; }
    }
}
