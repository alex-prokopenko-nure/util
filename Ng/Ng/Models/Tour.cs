using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateFormatted { get; set; }

        public int ClientId { get; set; }
        //public Client Client { get; set; }
        public int ExcursionId { get; set; }
        //public Excursion Excursion { get; set; }
    }
}
