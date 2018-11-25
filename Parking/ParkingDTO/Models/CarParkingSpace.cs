using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public class CarParkingSpace : AParkingSpace
    {
        private CarParkingSpace()
        {
            base.Type = "Voiture";
        }

        public CarParkingSpace(long id)
            : this()
        {
            base.IdNumber = id;
        }
    }
}
