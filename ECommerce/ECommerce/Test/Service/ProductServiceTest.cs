using ECommerce.Dto;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Test.Service
{
    public class ProductServiceTest
    {
        private readonly Mock<ProductRepositoryInterface> _productRepo;
        private readonly Mock<BrandRepositoryInterface> _brandRepo;
        private readonly Mock<TagRepositoryInterface> _tagRepo;
        private readonly Mock<CategoryRepositoryInterface> _categoryRepo;
        private readonly ProductService _productService;
        ProductCreateDto createDto;
        public ProductServiceTest()
        {
            _productRepo = new Mock<ProductRepositoryInterface>();
            _brandRepo = new Mock<BrandRepositoryInterface>();
            _tagRepo = new Mock<TagRepositoryInterface>();
            _categoryRepo = new Mock<CategoryRepositoryInterface>();

            _productService = new ProductService(_productRepo.Object,_categoryRepo.Object,_brandRepo.Object,_tagRepo.Object);
            createDto = new ProductCreateDto()
            {
                BrandId = 1,
                CategoryId = 1,
                Color = "Red",
                Description = "Description",
                Image = "/image",
                Name = "name",
                Price = 10,
                SKU = "sku",
                TagId = 1
            };

        }
        [Fact]
        public async Task Test_Create_Method_Creates_Correct_Product()
        {
            Brand brand = new Brand("Brand");
            Category category = new Category("category");
            Tag tag = new Tag("tag");
            _productRepo.Setup(a => a.GetBySKU(createDto.SKU)).ReturnsAsync(null as Product);
            _brandRepo.Setup(a => a.GetById(createDto.BrandId)).ReturnsAsync(brand);
            _tagRepo.Setup(a => a.GetById(createDto.TagId)).ReturnsAsync(tag);
            _categoryRepo.Setup(a => a.GetById(createDto.CategoryId)).ReturnsAsync(category);

            var product = await _productService.Create(createDto);

            Assert.Same(brand, product.Brand);
            Assert.Same(category, product.Category);
            Assert.Same(tag, product.Tag);
            Assert.Equal("name", product.Name);
            Assert.Equal(10, product.Price);
            Assert.Equal("Description", product.Description);
            Assert.Equal("Red", product.Color);
            Assert.Equal("/image", product.Image);
            Assert.Equal(Product.AvailabilityStatusAvailable, product.AvailabilityStatus);
        }
    }
}
