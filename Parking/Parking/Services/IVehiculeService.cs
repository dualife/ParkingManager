using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingIHM.ObservableModel;

namespace ParkingIHM.Services
{
    public interface IVehiculeService
    {
        ObservableVehicule GetNewVehicule(string vehiculeType);
    }
}
