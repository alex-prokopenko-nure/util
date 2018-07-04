﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface IExcursionRepository
    {
        Task<Excursion> Get(int id);
        Task<List<Excursion>> GetAll();
        Task<Excursion> Create(Excursion excursion);
        void Delete(int id);
    }
}