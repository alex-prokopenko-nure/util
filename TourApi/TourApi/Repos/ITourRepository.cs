using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface ITourRepository
    {
        Task<Tour> Get(int id);
        Task<List<Tour>> GetAll();
        Task<Tour> Create(Tour tour);
        Task<Tour> Update(Tour tour);
        Task<int> Delete(int id);
    }
}
