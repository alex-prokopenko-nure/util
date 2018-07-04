using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourApi.Models
{
    public class Excursion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Tour> Tours { get; set; }
        public List<ExcursionSight> ExcursionSights { get; set; }
    }
}
