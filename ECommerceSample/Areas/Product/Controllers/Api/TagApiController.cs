﻿using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Repository;
using ECommerce.Service;
using ECommerceSample.Areas.Product.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSample.Areas.Product.Controllers.Api
{
    [Route("api/product/tags")]
    public class TagApiController : ControllerBase
    {
        private readonly TagRepositoryInterface _tagRepo;
        private readonly TagServiceInterface _tagService;
        private readonly ILogger<TagApiController> _logger;
        public TagApiController(TagRepositoryInterface tagRepo, ILogger<TagApiController> logger, TagServiceInterface tagService)
        {
            _tagRepo = tagRepo;
            _logger = logger;
            _tagService = tagService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Tags = await _tagRepo.GetQueryable().OrderBy(a => a.TagId).ToListAsync();
                var Datas = new List<TagIndexViewModel>();
                foreach (var tag in Tags)
                {
                    Datas.Add(new TagIndexViewModel()
                    {
                        Id = tag.TagId,
                        Name = tag.Name
                    });
                }
                return Ok(Datas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var Tag = await _tagRepo.GetById(id).ConfigureAwait(true) ?? throw new TagNotFoundException();
                var Data = new TagIndexViewModel()
                {
                    Id = Tag.TagId,
                    Name = Tag.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(TagCreateViewModel model)
        {
            try
            {
                var Dto = new TagCreateDto()
                {
                    Name = model.Name
                };
                var Tag = await _tagService.Create(Dto);
                var Data = new TagIndexViewModel()
                {
                    Id = Tag.TagId,
                    Name = Tag.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TagUpdateViewModel model)
        {
            try
            {
                var Tag = await _tagRepo.GetById(id).ConfigureAwait(true) ?? throw new TagNotFoundException();

                var Dto = new TagUpdateDto()
                {
                    Name = model.Name,
                    TagId = id
                };
                await _tagService.Update(Dto);
                var Data = new TagIndexViewModel()
                {
                    Id = Tag.TagId,
                    Name = Tag.Name
                };

                return Ok(Data);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}