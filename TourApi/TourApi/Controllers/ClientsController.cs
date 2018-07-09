using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private IClientsRepository _clientRepository;

        public ClientsController(IClientsRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
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