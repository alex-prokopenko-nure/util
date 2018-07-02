using Ng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Data
{
    public class ClientData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ClientData(Client client)
        {
            Id = client.Id;
            Name = client.Name;
        }
    }
}
