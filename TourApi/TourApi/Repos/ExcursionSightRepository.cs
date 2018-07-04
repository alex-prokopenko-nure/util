﻿using System;
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

        public async Task<ExcursionSight> Create(Guid excursionId, Guid sightId)
        {
            ExcursionSight exs = new ExcursionSight { ExcursionId = excursionId, SightId = sightId };
            await _dbContext.ExcursionSights.AddAsync(exs);
            await _dbContext.SaveChangesAsync();
            return exs;
        }

        public async Task<Tuple<Guid, Guid>> Delete(Guid excursionId, Guid sightId)
        {
            _dbContext.ExcursionSights.Remove(await _dbContext.ExcursionSights.FirstOrDefaultAsync(x => x.ExcursionId == excursionId && x.SightId == sightId));
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
