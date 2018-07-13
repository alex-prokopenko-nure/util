using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Tuple<Guid, Guid> result;
            try
            {
                result = await _excursionSightRepository.Delete(excursionId, sightId);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
