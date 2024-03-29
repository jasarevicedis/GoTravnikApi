﻿using AutoMapper;
using GoTravnikApi.Dto.RequestDto;
using GoTravnikApi.Dto.ResponseDto;
using GoTravnikApi.Exceptions;
using GoTravnikApi.IServices;
using GoTravnikApi.Models;
using GoTravnikApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoTravnikApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class TouristContentController<TouristContentType, TouristContentRequestDtoType, TouristContentResponseDtoType> : ControllerBase
        where TouristContentType : TouristContent
        where TouristContentRequestDtoType : TouristContentDtoRequest
        where TouristContentResponseDtoType : TouristContentDtoResponse
    {
        private readonly ITouristContentService<TouristContentType, TouristContentRequestDtoType, TouristContentResponseDtoType> _touristContentService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IRatingService _ratingService;
        private readonly string _controllerRouteName;
        public TouristContentController(ITouristContentService<TouristContentType, TouristContentRequestDtoType, TouristContentResponseDtoType> touristContentService, ISubcategoryService subcategoryService, IRatingService ratingService, string controllerRouteName)
        {
            _touristContentService = touristContentService;
            _subcategoryService = subcategoryService;
            _ratingService = ratingService;
            _controllerRouteName = controllerRouteName;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<TouristContentResponseDtoType>>> GetAll()
        {
            try
            {
                var touristContentResponseDtos = await _touristContentService.GetAll();

                return Ok(touristContentResponseDtos);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TouristContentResponseDtoType>> GetById(int id)
        {
            try
            {
                var touristContentResponseDto = await _touristContentService.GetById(id);

                return Ok(touristContentResponseDto);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }

        [HttpGet("{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<TouristContentResponseDtoType>>> GetByName(string name)
        {
            try
            {
                var touristContentResponseDtos = await _touristContentService.GetByName(name);

                return Ok(touristContentResponseDtos);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }

        }

        [HttpGet("sort/name/{sort_order}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<TouristContentDtoResponse>>> GetSortedByName(string sort_order)
        {
            if (sort_order != null && !sort_order.Equals("asc") && !sort_order.Equals("desc"))
            {
                return BadRequest("Sort order can only be \'asc\', \'desc\' or it can be ommited - (asc is default)");
            }
            try
            {
                var touristContentResponseDtos = await _touristContentService.SortByName(sort_order);

                return Ok(touristContentResponseDtos);
            }
            catch (InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }

        [HttpGet("filter/subcategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<AccommodationDtoResponse>>> GetFilteredBySubcategories([FromQuery] List<string> subcategory_names)
        {
            try
            {
                var touristContentResponseDtos = await _touristContentService.GetBySubcategories(subcategory_names);

                return Ok(touristContentResponseDtos);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Add([FromBody] TouristContentRequestDtoType touristContentRequestDto)
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var touristContentId = await _touristContentService.Add(touristContentRequestDto);
                return Created($"{_controllerRouteName}/{touristContentId}", "Successfully added entity");
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode, 
                    title: "Internal Server Error", 
                    detail: ex.Message);
            }

        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _touristContentService.Delete(id);
                return Ok("Successfully deleted entity");
            }
            catch(NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch(InternalServerErrorException ex)
            {
                return Problem
                    (statusCode: (int)ex.HttpStatusCode,
                    title: "Internal Server Error",
                    detail: ex.Message);
            }
        }
    }
}
