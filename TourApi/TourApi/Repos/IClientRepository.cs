using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public interface IClientRepository
    {
        Task<Client> Get(Guid id);
        Task<List<Client>> GetAll();
        Task<Client> Create(Client client);
        void Delete(Guid id);
    }
}
