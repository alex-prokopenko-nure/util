using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class TourRepository : ITourRepository
    {
        private ApplicationDbContext _dbContext;

        public TourRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tour> Create(Tour tour)
        {
            await _dbContext.Tours.AddAsync(tour);
            await _dbContext.SaveChangesAsync();
            return tour;
        }

        public async Task<int> Delete(int id)
        {
            _dbContext.Tours.Remove(await _dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id));
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Tour> Get(int id)
        {
            return await _dbContext.Tours.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Tour>> GetAll()
        {
            return await _dbContext.Tours.ToListAsync();
        }

        public async Task<Tour> Update(Tour tour)
        {
            Tour updatedTour = await _dbContext.Tours.FirstOrDefaultAsync(x => x.Id == tour.Id);
            updatedTour.Date = tour.Date;
            updatedTour.ExcursionId = tour.ExcursionId;
            updatedTour.ClientId = tour.ClientId;
            await _dbContext.SaveChangesAsync();
            return updatedTour;
        }
    }
}
