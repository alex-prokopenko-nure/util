using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Data
{
    public class SightData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SightData(Sight sight)
        {
            Id = sight.Id;
            Name = sight.Name;
        }
    }
}
