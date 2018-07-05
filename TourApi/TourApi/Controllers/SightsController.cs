using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Route("[controller]")]
    public class SightsController : Controller
    {
        private ISightsRepository _sightRepository;

        public SightsController(ISightsRepository sightRepository)
        {
            _sightRepository = sightRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSights()
        {
            return Ok(await _sightRepository.GetAllSights());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSights(Guid id)
        {
            return Ok(await _sightRepository.GetSights(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddSight([FromBody]Sight sight)
        {
            return Ok(await _sightRepository.Create(sight));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _sightRepository.Delete(id));
        }
    }
}
