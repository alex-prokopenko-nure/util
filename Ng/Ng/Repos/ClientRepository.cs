using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ng.Models;

namespace Ng.Repos
{
    public class ClientRepository : IClientRepository
    {
        private ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Contains(Client client)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == client.Id) != null;
        }

        public void Create(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public Client Get(int id)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == id);
        }

        public List<Client> GetAll()
        {
            return _dbContext.Clients.ToList();
        }
    }
}
