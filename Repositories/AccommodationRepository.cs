﻿using GoTravnikApi.Data;
using GoTravnikApi.Dto;
using GoTravnikApi.Interfaces;
using GoTravnikApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GoTravnikApi.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly DataContext _dataContext;
        public AccommodationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AccomodationExists(int id)
        {
            return await _dataContext.Accomodation.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> AddAccommodation(Accommodation accommodation)
        {
            await _dataContext.AddAsync(accommodation.Location);

            await _dataContext.AddAsync(accommodation);

            return await Save();
        }

        public async Task<Accommodation> GetAccommodation(int id)
        {
            return await _dataContext.Accomodation
                .Where(a => a.Id == id)
                .Include(a => a.Location)
                .Include(a => a.Ratings)
                .Include(a => a.Subcategories)
               .FirstOrDefaultAsync();
        }

        public async Task<List<Accommodation>> GetAccomodations()
        {
            return await _dataContext.Accomodation
            .Include(x => x.Location)
            .Include(x => x.Ratings)
            .Include(x => x.Subcategories)
            .ToListAsync();
        }

        public async Task<List<Accommodation>> GetAccomodations(string searchName)
        {
            return await _dataContext.Accomodation
                .Where(a => a.Name.ToLower().Contains(searchName.ToLower()))
                .Include(x => x.Location)
                .Include(x => x.Ratings)
                .Include(x => x.Subcategories)
                .ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved =await _dataContext.SaveChangesAsync();
            return saved > 0;
        }
    }
}
