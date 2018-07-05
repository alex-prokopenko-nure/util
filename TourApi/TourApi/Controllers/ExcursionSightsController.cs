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
    public class ExcursionSightsController : Controller
    {
        private IExcursionSightRepository _excursionSightRepository;

        public ExcursionSightsController(IExcursionSightRepository excursionSightRepository)
        {
            _excursionSightRepository = excursionSightRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSights()
        {
            return Ok(await _excursionSightRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSights(Guid id)
        {
            return Ok(await _excursionSightRepository.GetExcursionSights(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddExcursionSight([FromBody] Pair ids)
        {
            return Ok(await _excursionSightRepository.Create(ids.ExcursionId, ids.SightId));
        }

        [HttpDelete("{excursionId}/{sightId}")]
        public async Task<IActionResult> DeleteExcursionSight(Guid excursionId, Guid sightId)
        {
            return Ok(await _excursionSightRepository.Delete(excursionId, sightId));
        }

        public class Pair
        {
            public Guid ExcursionId { get; set; }
            public Guid SightId { get; set; }
        }
    }
}
