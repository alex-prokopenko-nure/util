using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ng.Models;

namespace Ng.Data
{
    public class TourData
    {
        public string Name { get; set; }
        public string DateFormatted { get; set; }
        public ExcursionData Excursion { get; set; }
        public ClientData Client { get; set; }

        public TourData(Tour tour)
        {
            this.Name = tour.Name;
            this.DateFormatted = tour.DateFormatted;
        }
    }
}
