using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;

namespace TourApi.ViewModels
{
    public class User
    {
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
    }
}
