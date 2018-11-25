using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public class Bike : Vehicle
    {
        public Bike()
        {
            base.Type = "Moto";
        }
    }
}
