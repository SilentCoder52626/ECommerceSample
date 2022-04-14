using ECommerce.Entity;
using Xunit;

namespace ECommerce.Test.Entity
{
    public class ProductTest
    {
        Brand brand = new Brand("Brand");
        Category category = new Category("Category");
        Tag tag = new Tag("tag");

        [Fact]
        public void Test_Product_Entity_Is_Created_With_Correct_Data()
        {
            Product product = new Product(category,brand,tag,"name",10,"Red","sku","/images/kk","Description");
            Assert.Same(brand,product.Brand);
            Assert.Same(category, product.Category);
            Assert.Same(tag, product.Tag);
            Assert.Equal("name", product.Name);
            Assert.Equal(10, product.Price);
            Assert.Equal("Description", product.Description);
            Assert.Equal("Red", product.Color);
            Assert.Equal("/images/kk", product.Image);
            Assert.Equal(Product.AvailabilityStatusAvailable, product.AvailabilityStatus);

        }
        [Fact]
        public void Test_Product_Set_As_UnAvailable_Sets_Available_Status_To_UnAvailable()
        {
            Product product = new Product(category, brand, tag, "name", 10, "Red", "sku", "/images/kk", "Description");
            product.SetAsUnAvailable();
            Assert.Equal(Product.AvailabilityStatusUnAvailable, product.AvailabilityStatus);
        }
        [Fact]
        public void Test_Product_Set_As_Available_Sets_Available_Status_To_UnAvailable()
        {
            Product product = new Product(category, brand, tag, "name", 10, "Red", "sku", "/images/kk", "Description");
            product.SetAsAvailable();
            Assert.Equal(Product.AvailabilityStatusAvailable, product.AvailabilityStatus);
        }
    }
}
