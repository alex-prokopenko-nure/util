using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Client result = await _clientRepository.Get(id);
            if(result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody]Client client)
        {
            Client result;
            try
            {
                result = await _clientRepository.Create(client);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }


    }
}