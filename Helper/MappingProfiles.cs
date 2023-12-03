﻿using AutoMapper;
using GoTravnikApi.Dto;
using GoTravnikApi.Models;

namespace GoTravnikApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Subcategory, SubcategoryDto>();
            CreateMap<SubcategoryDto, Subcategory>();

            CreateMap<Accommodation, AccommodationDtoResponse>();
            CreateMap<AccommodationDtoResponse, Accommodation>();
            CreateMap<Accommodation, AccommodationDtoRequest>();
            CreateMap<AccommodationDtoRequest, Accommodation>();

            
        }
    }
}
