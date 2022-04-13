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
    public class TagService : TagServiceInterface
    {
        private readonly TagRepositoryInterface _tagRepo;

        public TagService(TagRepositoryInterface TagRepo)
        {
            _tagRepo = TagRepo;
        }

        public async Task<Tag> Create(TagCreateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            ValidateName(dto.Name);
            var Tag = new Tag(dto.Name);
            await _tagRepo.Insert(Tag).ConfigureAwait(false);
            Tx.Complete();
            return Tag;
        }

        public async Task Update(TagUpdateDto dto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            var Tag = await _tagRepo.GetById(dto.TagId).ConfigureAwait(false) ?? throw new TagNotFoundException();
            ValidateName(dto.Name,Tag);
            Tag.Update(dto.Name);
            await _tagRepo.Update(Tag).ConfigureAwait(false);
            Tx.Complete();
        }

        private async void ValidateName(string name,Tag? Tag=null)
        {
            var TagByName = await _tagRepo.GetByName(name).ConfigureAwait(false);
            if(TagByName != Tag && TagByName != null )
            {
                throw new DuplicateTagNameException(name);
            }
        }
    }
}
