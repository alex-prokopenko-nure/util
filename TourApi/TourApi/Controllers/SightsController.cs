using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Authorize]
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
            var result = await _sightRepository.GetSights(id);
            if(result.Count != 0)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddSight([FromBody]Sight sight)
        {
            Sight result;
            try
            {
                result = await _sightRepository.Create(sight);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
