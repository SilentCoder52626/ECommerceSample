namespace ECommerceSample.Models
{
    public class HomeViewModel
    {
        public IList<ProductListModel> ProductListModel { get; set; } = new List<ProductListModel>();

    }
    public class ProductListModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
