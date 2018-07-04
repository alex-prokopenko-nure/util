using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface ISightRepository
    {
        Task<List<Sight>> GetAllSights();
        Task<List<Sight>> GetSights(Guid id);
        Task<Sight> Create(Sight sight);
        Task<Guid> Delete(Guid id);
    }
}
