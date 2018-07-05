using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers
{
    [Route("[controller]")]
    public class ExcursionsController : Controller
    {
        private IExcursionsRepository _excursionRepository;

        public ExcursionsController(IExcursionsRepository excursionRepository)
        {
            _excursionRepository = excursionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetExcursions()
        {
            return Ok(await _excursionRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExcursion(Guid id)
        {
            return Ok(await _excursionRepository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddExcursion([FromBody]Excursion excursion)
        {
            return Ok(await _excursionRepository.Create(excursion));
        }

        [HttpDelete("{id}")]
        public Guid DeleteExcursion(Guid id)
        {
            _excursionRepository.Delete(id);
            return id;
        }
    }
}