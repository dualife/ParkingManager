using GalaSoft.MvvmLight;
using ParkingDTO.Models;

namespace ParkingIHM.ObservableModel
{
    public class ObservableVehicule : ObservableObject
    {
        private string _numberplate;
        private string _type;

        private ObservableVehicule()
        {
        }

        public ObservableVehicule(Vehicle vehicle)
            : this()
        {
            this.Numberplate = vehicle?.Numberplate;
            this.Type = vehicle?.Type;
        }

        public string Numberplate
        {
            get { return _numberplate; }
            set { this.Set(ref this._numberplate, value); }
        }

        public string Type
        {
            get { return _type; }
            set { this.Set(ref this._type, value); }
        }
    }
}