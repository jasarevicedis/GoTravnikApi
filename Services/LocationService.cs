﻿using AutoMapper;
using GoTravnikApi.Dto.RequestDto;
using GoTravnikApi.Dto.ResponseDto;
using GoTravnikApi.IRepositories;
using GoTravnikApi.IServices;
using GoTravnikApi.Models;

namespace GoTravnikApi.Services
{
    public class LocationService : Service<Location, LocationDtoRequest, LocationDtoResponse>, ILocationService
    {
        public LocationService(ILocationRepository locationRepository, IMapper mapper) : base(locationRepository, mapper)
        {
        }
    }
}
