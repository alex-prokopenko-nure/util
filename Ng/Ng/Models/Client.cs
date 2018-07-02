using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tour> Tours { get; set; }
    }
}
