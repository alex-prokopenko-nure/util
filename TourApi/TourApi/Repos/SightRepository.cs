using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class SightRepository : ISightRepository
    {
        private ApplicationDbContext _dbContext;

        public SightRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sight> Create(Sight sight)
        {
            await _dbContext.Sights.AddAsync(sight);
            await _dbContext.SaveChangesAsync();
            return sight;
        }

        public async Task<int> Delete(int id)
        {
            _dbContext.Sights.Remove(await _dbContext.Sights.FirstOrDefaultAsync(x => x.Id == id));
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<Sight>> GetAllSights()
        {
           return await _dbContext.Sights.ToListAsync();
        }

        public async Task<List<Sight>> GetSights(int id)
        {
            List<int> ids = await _dbContext.ExcursionSights.Where(x => x.ExcursionId == id).Select(x => x.SightId).ToListAsync();
            return await _dbContext.Sights.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
