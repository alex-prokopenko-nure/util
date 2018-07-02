using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ng.Models;

namespace Ng.Repos
{
    public class ExcursionRepository : IExcursionRepository
    {
        private ApplicationDbContext _dbContext;

        public ExcursionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Contains(Excursion excursion)
        {
            return _dbContext.Excursions.FirstOrDefault(x => x.Id == excursion.Id) != null;
        }

        public void Create(Excursion excursion)
        {
            _dbContext.Excursions.Add(excursion);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.Excursions.Remove(_dbContext.Excursions.FirstOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public Excursion Get(int id)
        {
            return _dbContext.Excursions.FirstOrDefault(x => x.Id == id);
        }

        public List<Excursion> GetAll()
        {
            return _dbContext.Excursions.ToList();
        }

        public void Update(Excursion excursion)
        {
            Excursion updatedExcursion = _dbContext.Excursions.FirstOrDefault(x => x.Id == excursion.Id);
            updatedExcursion.Name = excursion.Name;
            _dbContext.SaveChanges();
        }
    }
}
