using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ng.Models;
using Ng.Repos;

namespace Ng.Controllers
{
    [Route("api/[controller]")]
    public class ExcursionController : Controller
    {
        private IExcursionRepository _excursionRepository;

        public ExcursionController(IExcursionRepository excursionRepository)
        {
            _excursionRepository = excursionRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Excursion> GetExcursions()
        {
            return _excursionRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Excursion GetExcursion(int id)
        {
            return _excursionRepository.Get(id);
        }

        [HttpPost]
        public Excursion AddExcursion([FromBody]Excursion excursion)
        {
            _excursionRepository.Create(excursion);
            return excursion;
        }

        [HttpPut("{id}")]
        public Excursion ChangeExcursion(int id, [FromBody]Excursion excursion)
        {
            _excursionRepository.Update(excursion);
            return excursion;
        }

        [HttpDelete("{id}")]
        public decimal DeleteExcursion(decimal id)
        {
            _excursionRepository.Delete(Convert.ToInt32(id));
            return id;
        }
    }
}