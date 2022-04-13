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
    public class BrandService : BrandServiceInterface
    {
        private readonly BrandRepositoryInterface _brandRepo;

        public BrandService(BrandRepositoryInterface brandRepo)
        {
            _brandRepo = brandRepo;
        }

        public async Task<Brand> Create(BrandCreateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            ValidateName(dto.Name);
            var Brand = new Brand(dto.Name);
            await _brandRepo.Insert(Brand).ConfigureAwait(false);
            Tx.Complete();
            return Brand;
        }

        public async Task Update(BrandUpdateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Brand = await _brandRepo.GetById(dto.BrandId).ConfigureAwait(false) ?? throw new BrandNotFoundException();
            ValidateName(dto.Name,Brand);
            Brand.Update(dto.Name);
            await _brandRepo.Update(Brand).ConfigureAwait(false);
            Tx.Complete();
        }

        private async void ValidateName(string name,Brand? brand=null)
        {
            var BrandByName = await _brandRepo.GetByName(name).ConfigureAwait(false);
            if(BrandByName != brand && BrandByName != null)
            {
                throw new DuplicateBrandNameException(name);
            }
        }
    }
}
