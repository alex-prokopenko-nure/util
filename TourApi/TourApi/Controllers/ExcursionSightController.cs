using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Route("[controller]")]
    public class ExcursionSightController : Controller
    {
        private IExcursionSightRepository _excursionSightRepository;

        public ExcursionSightController(IExcursionSightRepository excursionSightRepository)
        {
            _excursionSightRepository = excursionSightRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSights()
        {
            return Ok(await _excursionSightRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSights(int id)
        {
            return Ok(await _excursionSightRepository.GetExcursionSights(id));
        }

        [HttpPost("{excursionId}")]
        public async Task<IActionResult> AddExcursionSight(int excursionId, [FromBody] int sightId)
        {
            return Ok(await _excursionSightRepository.Create(excursionId, sightId));
        }

        [HttpDelete("{excursionId}/{sightId}")]
        public async Task<IActionResult> DeleteExcursionSight(int excursionId, int sightId)
        {
            return Ok(await _excursionSightRepository.Delete(excursionId, sightId));
        }
    }
}
