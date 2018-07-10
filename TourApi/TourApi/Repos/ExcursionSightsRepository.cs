using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TourApi.Repos
{
    public class ExcursionSightsRepository : IExcursionSightRepository
    {
        private ApplicationDbContext _dbContext;

        public ExcursionSightsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool Contains(ExcursionSight exs)
        {
            return _dbContext.ExcursionSights.FirstOrDefault(x => x.ExcursionId == exs.ExcursionId && x.SightId == exs.SightId) != null;
        }

        public async Task<ExcursionSight> Create(Guid excursionId, Guid sightId)
        {
            ExcursionSight exs = new ExcursionSight { ExcursionId = excursionId, SightId = sightId };
            if (!Contains(exs))
            {
                await _dbContext.ExcursionSights.AddAsync(exs);
                await _dbContext.SaveChangesAsync();
                return exs;
            }
            return null;
        }

        public async Task<Tuple<Guid, Guid>> Delete(Guid excursionId, Guid sightId)
        {
            ExcursionSight exs = new ExcursionSight { ExcursionId = excursionId, SightId = sightId };
            _dbContext.ExcursionSights.Attach(exs);
            _dbContext.ExcursionSights.Remove(exs);
            await _dbContext.SaveChangesAsync();
            return new Tuple<Guid, Guid>(excursionId, sightId);
        }

        public async Task<List<ExcursionSight>> GetAll()
        {
            return await _dbContext.ExcursionSights.ToListAsync();
        }

        public async Task<List<Guid>> GetExcursionSights(Guid excursionId)
        {
            return await _dbContext.ExcursionSights.Where(x => x.ExcursionId == excursionId).Select(x => x.SightId).ToListAsync();
        }
    }
}
