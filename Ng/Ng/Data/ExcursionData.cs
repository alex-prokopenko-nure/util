using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Data
{
    public class ExcursionData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ExcursionData(Excursion excursion)
        {
            Id = excursion.Id;
            Name = excursion.Name;
        }
    }
}
