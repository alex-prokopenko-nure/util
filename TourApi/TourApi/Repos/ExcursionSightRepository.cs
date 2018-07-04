using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class ExcursionSightRepository : IExcursionSightRepository
    {
        private ApplicationDbContext _dbContext;

        public ExcursionSightRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExcursionSight> Create(int excursionId, int sightId)
        {
            ExcursionSight exs = new ExcursionSight { ExcursionId = excursionId, SightId = sightId };
            await _dbContext.ExcursionSights.AddAsync(exs);
            await _dbContext.SaveChangesAsync();
            return exs;
        }

        public async Task<Tuple<int, int>> Delete(int excursionId, int sightId)
        {
            _dbContext.ExcursionSights.Remove(await _dbContext.ExcursionSights.FirstOrDefaultAsync(x => x.ExcursionId == excursionId && x.SightId == sightId));
            await _dbContext.SaveChangesAsync();
            return new Tuple<int, int>(excursionId, sightId);
        }

        public async Task<List<ExcursionSight>> GetAll()
        {
            return await _dbContext.ExcursionSights.ToListAsync();
        }

        public async Task<List<int>> GetExcursionSights(int excursionId)
        {
            return await _dbContext.ExcursionSights.Where(x => x.ExcursionId == excursionId).Select(x => x.SightId).ToListAsync();
        }
    }
}
