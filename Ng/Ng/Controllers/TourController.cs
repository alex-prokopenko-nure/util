using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ng.Models;
using Microsoft.EntityFrameworkCore;
using Ng.Data;
using Ng.Repos;

namespace Ng.Controllers
{
    [Route("api/[controller]")]
    public class TourController : Controller
    {
        private ITourRepository _tourRepository;

        public TourController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Tour> GetTours()
        {
            return _tourRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Tour GetTour(int id)
        {
            return _tourRepository.Get(id);
        }

        [HttpPost]
        public Tour AddTour([FromBody]Tour tour)
        {
            _tourRepository.Create(tour);
            return tour;
        }

        [HttpPut("{id}")]
        public Tour ChangeTour(int id, [FromBody]Tour tour)
        {
            _tourRepository.Update(tour);
            return tour;
        }

        [HttpDelete("{id}")]
        public decimal DeleteTour(decimal id)
        {
            _tourRepository.Delete(Convert.ToInt32(id));
            return id;
        }
    }
}
