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
            var result = await _excursionRepository.Get(id);
            if(result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddExcursion([FromBody]Excursion excursion)
        {
            Excursion result;
            try
            {
                result = await _excursionRepository.Create(excursion);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}