using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public class Vehicle
    {
        // Create pseudo random immatriculation
        public string Numberplate => Guid.NewGuid().ToString().Substring(0, 6);
        public string Type { get; protected set; }
    }
}
