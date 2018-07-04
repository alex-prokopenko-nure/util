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
        Task<List<Guid>> GetExcursionSights(Guid excursionId);
        Task<ExcursionSight> Create(Guid excursionId, Guid sightId);
        Task<Tuple<Guid, Guid>> Delete(Guid excursionId, Guid sightId);
    }
}
