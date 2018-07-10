using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Repos;
using TourApi.ViewModels;

namespace TourApi.Controllers
{
    [Authorize]
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
            var result = await _excursionSightRepository.Create(ids.ExcursionId, ids.SightId);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{excursionId}/{sightId}")]
        public async Task<IActionResult> DeleteExcursionSight(Guid excursionId, Guid sightId)
        {
            return Ok(await _excursionSightRepository.Delete(excursionId, sightId));
        }
    }
}
