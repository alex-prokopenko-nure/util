using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ng.Models;
using Ng.Repos;

namespace Ng.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Client> GetClients()
        {
            return _clientRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Client GetClient(int id)
        {
            return _clientRepository.Get(id);
        }

        [HttpPost]
        public Client AddClient([FromBody]Client client)
        {
            _clientRepository.Create(client);
            return client;
        }
    }
}