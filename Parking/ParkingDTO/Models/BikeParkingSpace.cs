using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public class BikeParkingSpace : AParkingSpace 
    {
        private BikeParkingSpace()
        {
            base.Type = "Moto";
        }

        public BikeParkingSpace(long id)
            : this()
        {
            base.IdNumber = id;
        }
    }
}
