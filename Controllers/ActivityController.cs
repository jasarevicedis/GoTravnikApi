﻿using AutoMapper;
using GoTravnikApi.Dto;
using GoTravnikApi.Interfaces;
using GoTravnikApi.Models;
using GoTravnikApi.Repositories;
using GoTravnikApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoTravnikApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        public ActivityController(IActivityRepository activityRepository, ISubcategoryRepository subcategoryRepository, IRatingRepository ratingRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _subcategoryRepository = subcategoryRepository;
            _ratingRepository = ratingRepository;   
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ActivityDtoResponse>))]
        public async Task<ActionResult<List<ActivityDtoResponse>>> GetActivities()
        {
            var activityDtos = await _activityRepository.GetActivities();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activityDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(ActivityDtoResponse))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ActivityDtoResponse>> GetActivity(int id)
        {
            if (!await _activityRepository.ActivityExists(id))
                return NotFound(ModelState);
            var activityDto = _mapper.Map<ActivityDtoResponse> (await _activityRepository.GetActivity(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activityDto);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(200, Type = typeof(List<ActivityDtoResponse>))]
        public async Task<ActionResult<List<ActivityDtoResponse>>> GetActivities(string name)
        {
            var activitiyDtos = _mapper.Map<List<ActivityDtoResponse>>(await _activityRepository.GetActivities(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activitiyDtos);
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ActivityDtoResponse>>> GetFilteredAndOrderedActivities([FromQuery] List<string> subcategory,[FromQuery] string? sortOption)
        {
            var activityDtos = _mapper.Map<List<ActivityDtoResponse>>(await _activityRepository.FilterAndOrderActivities(subcategory, sortOption));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activityDtos);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddActivity([FromBody] ActivityDtoRequest activityDtoRequest)
        {
            if (activityDtoRequest == null)
                return BadRequest(ModelState);

            if (activityDtoRequest.Location == null)
                return BadRequest(ModelState);

            if (!await _subcategoryRepository.SubcategoriesExist(activityDtoRequest.Subcategories.Select(x => x.Name).ToList()))
            {
                ModelState.AddModelError("error", "Subcategory does not exist in the database");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<Subcategory> subcategories = new List<Subcategory>();

            foreach (var subcategoryDto in activityDtoRequest.Subcategories)
            {
                var subcategory = await _subcategoryRepository.GetSubcategory(subcategoryDto.Name);
                subcategories.Add(subcategory);
            }
            Activity activity = _mapper.Map<Activity>(activityDtoRequest);
            activity.Subcategories = subcategories;

            if (!await _activityRepository.AddActivity(activity))
            {
                ModelState.AddModelError("error", "Database saving error");
                return StatusCode(500, ModelState);
            }

            return Ok ("Succesfully added");

        }

        [HttpPost("rating/{activityId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddRating(int activityId, [FromBody] RatingDtoRequest ratingDtoRequest)
        {
            if (ratingDtoRequest == null)
                return BadRequest(ModelState);

            if (!await _activityRepository.ActivityExists(activityId))
            {
                ModelState.AddModelError("error", "Activity does not exist in the database");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var activity = await _activityRepository.GetActivity(activityId);
            var rating = _mapper.Map<Rating>(ratingDtoRequest);

            activity.Ratings.Add(rating);

            if (!await _ratingRepository.AddRating(rating))
            {
                ModelState.AddModelError("error", "Database updating error");
                return StatusCode(500, ModelState);
            }

            if (!await _activityRepository.UpdateActivity(activity))
            {
                ModelState.AddModelError("error", "Database updating error");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully added");

        }

    }
}
