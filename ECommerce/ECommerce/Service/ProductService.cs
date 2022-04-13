using ECommerce.Dto;
using ECommerce.Entity;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class ProductService : ProductServiceInterface
    {
        private readonly ProductRepositoryInterface _productRepo;
        private readonly CategoryRepositoryInterface _categoryRepo;
        private readonly BrandRepositoryInterface _brandRepo;
        private readonly TagRepositoryInterface _tagRepo;

        public ProductService(ProductRepositoryInterface productRepo, CategoryRepositoryInterface categoryRepo, BrandRepositoryInterface brandRepo, TagRepositoryInterface tagRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
            _tagRepo = tagRepo;
        }

        public async Task<Product> Create(ProductCreateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            ValidateSKU(dto.SKU);
            var Category = await _categoryRepo.GetById(dto.CategoryId).ConfigureAwait(false) ?? throw new CategoryNotFoundException();
            var Brand = await _brandRepo.GetById(dto.BrandId).ConfigureAwait(false) ?? throw new BrandNotFoundException();
            var Tag = await _tagRepo.GetById(dto.TagId).ConfigureAwait(false) ?? throw new TagNotFoundException();
            Product product= new(Category, Brand, Tag, dto.Name, dto.Price, dto.Color, dto.SKU, dto.Image, dto.Description);
            await _productRepo.Update(product).ConfigureAwait(false);
            Tx.Complete();
            return product;
        }

        public async Task SetAsAvailable(long id)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Product = await _productRepo.GetById(id).ConfigureAwait(false) ?? throw new ProductNotFoundException();
            Product.SetAsAvailable();
            await _productRepo.Update(Product).ConfigureAwait(false);
            Tx.Complete();
        }

        public async Task SetAsUnAvailable(long id)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Product = await _productRepo.GetById(id).ConfigureAwait(false) ?? throw new ProductNotFoundException();
            Product.SetAsUnAvailable();
            await _productRepo.Update(Product).ConfigureAwait(false);
            Tx.Complete();
        }

        public async Task Update(ProductUpdateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Product = await _productRepo.GetById(dto.ProductId).ConfigureAwait(false) ?? throw new ProductNotFoundException();
            ValidateSKU(dto.SKU, Product);
            var Category = await _categoryRepo.GetById(dto.CategoryId).ConfigureAwait(false) ?? throw new CategoryNotFoundException();
            var Brand = await _brandRepo.GetById(dto.BrandId).ConfigureAwait(false) ?? throw new BrandNotFoundException();
            var Tag = await _tagRepo.GetById(dto.TagId).ConfigureAwait(false) ?? throw new TagNotFoundException();
            Product.Update(Category,Brand,Tag,dto.Name,dto.Price,dto.Color,dto.SKU,dto.Image,dto.Description);
            await _productRepo.Update(Product).ConfigureAwait(false);
            Tx.Complete();
        }

        private async void ValidateSKU(string sku, Product? product=null)
        {
            var ProductOfSameSku = await _productRepo.GetBySKU(sku).ConfigureAwait(false);
            if(ProductOfSameSku != product && ProductOfSameSku != null)
            {
                throw new DuplicateProductSKUException(sku);
            }
        }

        public async Task UpdatePrice(long id, decimal price)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Product = await _productRepo.GetById(id).ConfigureAwait(false) ?? throw new ProductNotFoundException();
            Product.UpdatePrice(price);
            await _productRepo.Update(Product).ConfigureAwait(false);
            Tx.Complete();
        }
    }
}
