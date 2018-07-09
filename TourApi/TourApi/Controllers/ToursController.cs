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
            return Ok(await _tourRepository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddTour([FromBody]Tour tour)
        {
            return Ok( await _tourRepository.Create(tour));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeTour(Guid id, [FromBody]Tour tour)
        {
            return Ok(await _tourRepository.Update(tour));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(Guid id)
        {
            return Ok(await _tourRepository.Delete(id));
        }
    }
}
