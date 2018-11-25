using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public class Car : Vehicle
    {
        public Car()
        {
            base.Type = "Voiture";
        }
    }
}
