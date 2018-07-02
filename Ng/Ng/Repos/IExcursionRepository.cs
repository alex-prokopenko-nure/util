using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Repos
{
    public interface IExcursionRepository
    {
        Excursion Get(int id);
        List<Excursion> GetAll();
        bool Contains(Excursion excursion);
        void Create(Excursion excursion);
        void Update(Excursion excursion);
        void Delete(int id);
    }
}
