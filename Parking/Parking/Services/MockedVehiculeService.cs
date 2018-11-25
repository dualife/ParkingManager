using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingDTO.Models;
using ParkingIHM.ObservableModel;

namespace ParkingIHM.Services
{
    public class MockedVehiculeService : IVehiculeService
    {
        public ObservableVehicule GetNewVehicule(string vehiculeType)
        {
            Vehicle vehicule;
            switch (vehiculeType)
            {
                case "Voiture":
                    vehicule = new Car();
                    break;
                case "Moto":
                    vehicule = new Bike();
                    break;
                default:
                    throw new ArgumentException("Vehicule type: " + vehiculeType + " inconnu.");
            }

            return new ObservableVehicule(vehicule);
        }
    }
}
