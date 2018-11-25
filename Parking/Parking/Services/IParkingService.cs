using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingIHM.ObservableModel;

namespace ParkingIHM.Services
{
    public interface IParkingService
    {
        ObservableCollection<ObservableParkingSpace> GetParkingSpaces();
        void ParkAVehicule(ObservableCollection<ObservableParkingSpace> parkingSpaces, ObservableVehicule vehicule);
        void EmptyParkingSpace(ObservableParkingSpace space);
    }
}
