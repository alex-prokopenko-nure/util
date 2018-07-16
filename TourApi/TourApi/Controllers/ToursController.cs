using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourApi.Models;
using Microsoft.EntityFrameworkCore;
using TourApi.Repos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace TourApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ToursController : Controller
    {
        private IToursRepository _tourRepository;

        public ToursController(IToursRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            return Ok(await _tourRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTour(Guid id)
        {
            var result = await _tourRepository.Get(id);
            if(result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTour([FromBody]Tour tour)
        {
            Tour result;
            try
            {
                result = await _tourRepository.Create(tour);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeTour(Guid id, [FromBody]Tour tour)
        {
            var result = await _tourRepository.Update(id, tour);
            if(result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(Guid id)
        {
            Guid result;
            try
            {
                result = await _tourRepository.Delete(id);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
