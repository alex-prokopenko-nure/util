using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.Repos
{
    public class ClientRepository : IClientRepository
    {
        private ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Create(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        public async void Delete(Guid id)
        {
            Client client = new Client { Id = id };
            _dbContext.Clients.Attach(client);
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client> Get(Guid id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Client>> GetAll()
        {
            return await _dbContext.Clients.ToListAsync();
        }
    }
}
