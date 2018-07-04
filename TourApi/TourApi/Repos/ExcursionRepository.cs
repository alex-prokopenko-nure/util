﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public class ExcursionRepository : IExcursionRepository
    {
        private ApplicationDbContext _dbContext;

        public ExcursionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Excursion> Create(Excursion excursion)
        {
            await _dbContext.Excursions.AddAsync(excursion);
            await _dbContext.SaveChangesAsync();
            return excursion;
        }

        public async void Delete(int id)
        {
            _dbContext.Excursions.Remove(await _dbContext.Excursions.FirstOrDefaultAsync(x => x.Id == id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Excursion> Get(int id)
        {
            return await _dbContext.Excursions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Excursion>> GetAll()
        {
            return await _dbContext.Excursions.ToListAsync();
        }
    }
}