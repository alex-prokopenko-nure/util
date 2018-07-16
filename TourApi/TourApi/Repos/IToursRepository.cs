using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface IToursRepository
    {
        Task<Tour> Get(Guid id);
        Task<List<Tour>> GetAll();
        Task<Tour> Create(Tour tour);
        Task<Tour> Update(Guid id, Tour tour);
        Task<Guid> Delete(Guid id);
    }
}
