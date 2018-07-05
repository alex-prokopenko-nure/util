using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class SightsRepository : ISightsRepository
    {
        private ApplicationDbContext _dbContext;

        public SightsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sight> Create(Sight sight)
        {
            await _dbContext.Sights.AddAsync(sight);
            await _dbContext.SaveChangesAsync();
            return sight;
        }

        public async Task<Guid> Delete(Guid id)
        {
            Sight sight = new Sight { Id = id };
            _dbContext.Sights.Attach(sight);
            _dbContext.Sights.Remove(sight);
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<Sight>> GetAllSights()
        {
           return await _dbContext.Sights.ToListAsync();
        }

        public async Task<List<Sight>> GetSights(Guid id)
        {
            List<Guid> ids = await _dbContext.ExcursionSights.Where(x => x.ExcursionId == id).Select(x => x.SightId).ToListAsync();
            return await _dbContext.Sights.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
