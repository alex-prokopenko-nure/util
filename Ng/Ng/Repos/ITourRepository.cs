using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Repos
{
    public interface ITourRepository
    {
        Tour Get(int id);
        List<Tour> GetAll();
        bool Contains(Tour tour);
        void Create(Tour tour);
        void Update(Tour tour);
        void Delete(int id);
    }
}
