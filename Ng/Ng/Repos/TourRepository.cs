using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Repos
{
    public class TourRepository : ITourRepository
    {
        private ApplicationDbContext _dbContext;

        public TourRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Contains(Tour tour)
        {
            return _dbContext.Tours.FirstOrDefault(x => x.ClientId == tour.ClientId && x.ExcursionId == tour.ExcursionId) != null;
        }

        public void Create(Tour tour)
        {
            _dbContext.Tours.Add(tour);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _dbContext.Tours.Remove(_dbContext.Tours.FirstOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public Tour Get(int id)
        {
            return _dbContext.Tours.FirstOrDefault(x => x.Id == id);
        }

        public List<Tour> GetAll()
        {
            return _dbContext.Tours.ToList();
        }

        public void Update(Tour tour)
        {
            Tour updatedTour = _dbContext.Tours.FirstOrDefault(x => x.Id == tour.Id);
            updatedTour.Name = tour.Name;
            updatedTour.DateFormatted = tour.DateFormatted;
            updatedTour.ExcursionId = tour.ExcursionId;
            updatedTour.ClientId = tour.ClientId;
            _dbContext.SaveChanges();
        }
    }
}
