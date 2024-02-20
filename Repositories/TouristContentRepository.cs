﻿using GoTravnikApi.Data;
using GoTravnikApi.Exceptions;
using GoTravnikApi.IRepositories;
using GoTravnikApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GoTravnikApi.Repositories
{
    public abstract class TouristContentRepository<Entity> : Repository<Entity>, ITouristContentRepository<Entity> where Entity : TouristContent
    {
        private readonly DataContext _dataContext;
        public TouristContentRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Entity>> FilterBySubcategories(List<string> subcategoryNames)
        {
            try
            {
                var query = _dataContext.Set<Entity>().AsQueryable();
                foreach (var subcategory in subcategoryNames)
                    query = query.Where(x => x.Subcategories.Any(sub => sub.Name == subcategory) == true);
                return await query
                    .Include(x => x.Location)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("Internal server error occured");
            }
        }

        public async Task<List<Entity>> GetByName(string name)
        {
            try
            {
                return await _dataContext.Set<Entity>()
                    .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                    .Include(x => x.Location)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InternalServerErrorException("Internal server error occured");
            }
        }

        public async Task<List<Entity>> SortByName(string sortOrder)
        {
            try
            {
                var query = _dataContext.Set<Entity>().AsQueryable();

                if(sortOrder == "asc" || sortOrder == "")
                    query = query.OrderBy(x => x.Name);
                else if(sortOrder == "desc")
                    query = query.OrderByDescending(x => x.Name);

                return await query
                    .Include(x => x.Location)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InternalServerErrorException("Internal server error occured");
            }
        }
    }
}

