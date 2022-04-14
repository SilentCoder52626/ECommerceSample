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
    public class CategoryService : CategoryServiceInterface
    {
        private readonly CategoryRepositoryInterface _categoryRepo;

        public CategoryService(CategoryRepositoryInterface CategoryRepo)
        {
            _categoryRepo = CategoryRepo;
        }

        public async Task<Category> Create(CategoryCreateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            await ValidateName(dto.Name);
            var Category = new Category(dto.Name);
            await _categoryRepo.Insert(Category).ConfigureAwait(false);
            Tx.Complete();
            return Category;
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Category = await _categoryRepo.GetById(dto.CategoryId).ConfigureAwait(false) ?? throw new CategoryNotFoundException();
            await ValidateName(dto.Name,Category);
            Category.Update(dto.Name);
            await _categoryRepo.Update(Category).ConfigureAwait(false);
            Tx.Complete();
        }

        private async Task ValidateName(string name,Category? Category=null)
        {
            var CategoryByName = await _categoryRepo.GetByName(name).ConfigureAwait(false);
            if (CategoryByName != Category && CategoryByName != null)
            {
                throw new DuplicateCategoryNameException(name);
            }
        }
    }
}
