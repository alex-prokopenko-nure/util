using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            return Ok(await _clientRepository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody]Client client)
        {
            return Ok(await _clientRepository.Create(client));
        }


    }
}