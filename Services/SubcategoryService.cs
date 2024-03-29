﻿using AutoMapper;
using GoTravnikApi.Dto.RequestDto;
using GoTravnikApi.Dto.ResponseDto;
using GoTravnikApi.Exceptions;
using GoTravnikApi.IRepositories;
using GoTravnikApi.IServices;
using GoTravnikApi.Models;

namespace GoTravnikApi.Services
{
    public class SubcategoryService : Service<Subcategory, SubcategoryDtoRequest, SubcategoryDtoResponse>, ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper): base(subcategoryRepository,mapper)
        {
        }
        public async Task<SubcategoryDtoResponse> GetSubcategory(string name)
        {
            try
            {
                var subcategory = await _subcategoryRepository.GetSubcategory(name) ?? throw new NotFoundException($"Subcategory with name {name} does not exist in the database");
                var subcategoryResponseDto = _mapper.Map<SubcategoryDtoResponse>(subcategory);
                return subcategoryResponseDto;
            }
            catch (InternalServerErrorException)
            {
                throw;
            }
        }

        public async Task<bool> SubcategoriesExist(List<string> subcategoryNames)
        {
            try
            {
                return await _subcategoryRepository.SubcategoriesExist(subcategoryNames);
            }
            catch (InternalServerErrorException)
            {
                throw;
            }
        }
    }
}
