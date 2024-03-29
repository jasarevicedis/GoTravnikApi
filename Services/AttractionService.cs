﻿using AutoMapper;
using GoTravnikApi.Dto.RequestDto;
using GoTravnikApi.Dto.ResponseDto;
using GoTravnikApi.IRepositories;
using GoTravnikApi.IServices;
using GoTravnikApi.Models;

namespace GoTravnikApi.Services
{
    public class AttractionService
        : TouristContentService<Attraction, AttractionDtoRequest, AttractionDtoResponse>, IAttractionService
    {
        public AttractionService(IAttractionRepository attractionRepository, ISubcategoryRepository subcategoryRepository, IMapper mapper) : base(attractionRepository, subcategoryRepository, mapper)
        {
        }
    }
}
