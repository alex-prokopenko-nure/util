using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Repos
{
    public interface IClientRepository
    {
        Client Get(int id);
        List<Client> GetAll();
        bool Contains(Client client);
        void Create(Client Client);
    }
}
