using GalaSoft.MvvmLight;
using ParkingDTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingIHM.ObservableModel
{
    public class ObservableParkingSpace : ObservableObject
    {
        private long _id;
        private string _type;
        private bool _isAvailable;
        private ObservableVehicule _vehicule;

        private ObservableParkingSpace()
        {
        }

        public ObservableParkingSpace(AParkingSpace space)
            : this()
        {
            this.IdNumber = space.IdNumber;
            this.Type = space.Type;
            this.IsAvailable = space.IsAvailable;
            this.Vehicule = space?.Vehicle != null ? new ObservableVehicule(space.Vehicle) : null;
        }

        #region "Properties"

        public long IdNumber
        {
            get { return _id; }
            set { this.Set(ref this._id, value); }
        }

        public string Type
        {
            get { return _type; }
            set { this.Set(ref this._type, value); }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { this.Set(ref this._isAvailable, value); }
        }

        public ObservableVehicule Vehicule
        {
            get { return _vehicule; }
            set
            {
                this.Set(ref this._vehicule, value);
                this.IsAvailable = value == null;
            }
        }

        #endregion
    }
}
