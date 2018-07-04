using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface IExcursionSightRepository
    {
        Task<List<ExcursionSight>> GetAll();
        Task<List<int>> GetExcursionSights(int excursionId);
        Task<ExcursionSight> Create(int excursionId, int sightId);
        Task<Tuple<int, int>> Delete(int excursionId, int sightId);
    }
}
