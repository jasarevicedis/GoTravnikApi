﻿using AutoMapper;
using GoTravnikApi.Dto.RequestDto;
using GoTravnikApi.Dto.ResponseDto;
using GoTravnikApi.Exceptions;
using GoTravnikApi.IRepositories;
using GoTravnikApi.IServices;
using GoTravnikApi.Models;

namespace GoTravnikApi.Services
{
    public class RatingService: Service<Rating, RatingDtoRequest, RatingDtoResponse>, IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        public RatingService(IRatingRepository ratingRepository, IMapper mapper): base(ratingRepository, mapper)
        { 
            _ratingRepository = ratingRepository;   
            _mapper = mapper;
        }

        public async Task ApproveRating(int id)
        {
            try
            {
                var rating = await _ratingRepository.GetById(id) ?? throw new NotFoundException("Entity does not exist in the database");
                rating.Approved = true;
                try
                {
                    await _ratingRepository.Update(rating);
                }
                catch (InternalServerErrorException)
                {
                    throw;
                }
            }
            catch (InternalServerErrorException)
            {
                throw;
            }
        }

        public async Task<List<RatingWithTouristContentDtoResponse>> GetUnapproved()
        {
            try
            {
                var ratingWithTouristContentDtoResponses = _mapper.Map<List<RatingWithTouristContentDtoResponse>>(await _ratingRepository.GetUnapproved());

                return ratingWithTouristContentDtoResponses;
            }
            catch(InternalServerErrorException)
            {
                throw;
            }
        }
    }
}
