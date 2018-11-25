using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDTO.Models
{
    public abstract class  AParkingSpace
    {
        public long IdNumber { get; set; }
        public Vehicle Vehicle { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
