using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourApi.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }

        public int ClientId { get; set; }
        public int ExcursionId { get; set; }
    }
}
