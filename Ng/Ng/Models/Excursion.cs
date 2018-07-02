﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Models
{
    public class Excursion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tour> Tours { get; set; }
        public ICollection<ExcursionSight> ExcursionSights { get; set; }
    }
}
