using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParkingIHM.ObservableModel;
using ParkingDTO.Models;

namespace ParkingIHM.Services
{
    public class MockedParkingService : IParkingService
    {
        public ObservableCollection<ObservableParkingSpace> GetParkingSpaces()
        {
            // Mocking building of parking (Should be retrieved from database through webservices)
            var list = new List<AParkingSpace>();

            for (long i = 0; i < 150; i++)
            {
                if (i % 2 == 0 && i < 100)
                {
                    list.Add(new BikeParkingSpace(i + 1));
                }
                else
                {
                    list.Add(new CarParkingSpace(i + 1));
                }
            }

            // Service convert the retrieved items into observables to be used by IHM
            var spaces = (from space in list
                          select new ObservableParkingSpace(space)).ToList();

            return new ObservableCollection<ObservableParkingSpace>(spaces);
        }

        // Should update distant data storage through webservices
        public void ParkAVehicule(ObservableCollection<ObservableParkingSpace> parkingSpaces, ObservableVehicule vehicule)
        {
            var typedSpaces = parkingSpaces.Where(x => x.Type == vehicule.Type && x.IsAvailable).FirstOrDefault();

            typedSpaces.Vehicule = vehicule;
        }

        // Should update distant data storage through webservices
        public void EmptyParkingSpace(ObservableParkingSpace space)
        {
            space.Vehicule = null;
        }
    }
}
