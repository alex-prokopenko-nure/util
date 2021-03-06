﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class ToursRepository : IToursRepository
    {
        private ApplicationDbContext _dbContext;

        public ToursRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tour> Create(Tour tour)
        {
            await _dbContext.Tours.AddAsync(tour);
            await _dbContext.SaveChangesAsync();
            return tour;
        }

        public async Task<Guid> Delete(Guid id)
        {
            Tour tour = new Tour { Id = id };
            _dbContext.Tours.Attach(tour);
            _dbContext.Tours.Remove(tour);
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Tour> Get(Guid id)
        {
            return await _dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Tour>> GetAll()
        {
            return await _dbContext.Tours.ToListAsync();
        }

        public async Task<Tour> Update(Guid id, Tour tour)
        {
            Tour updatedTour = await _dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
            if (updatedTour != null)
            {
                updatedTour.Date = tour.Date;
                updatedTour.ExcursionId = tour.ExcursionId;
                updatedTour.ClientId = tour.ClientId;
                await _dbContext.SaveChangesAsync();
            }
            return updatedTour;
        }
    }
}
