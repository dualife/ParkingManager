using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ParkingIHM.ObservableModel;
using ParkingIHM.Services;
using ParkingIHM.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingIHM.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IParkingService _parkingService;
        private readonly IVehiculeService _vehiculeService;
        private ObservableCollection<ObservableParkingSpace> _parkingSpaces;
        private bool _isBusy;
        private RelayCommand _parkACarCmd;
        private RelayCommand _parkABikeCmd;
        private RelayCommand<ObservableParkingSpace> _leavingVehicleCmd;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IParkingService parkingService, IVehiculeService vehiculeService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this._parkingService = parkingService;
            this._vehiculeService = vehiculeService;
            Broker.Subscribe(this);

            this.GetParkingSpacesAsync();
        }

        #region "Properties"

        public ObservableCollection<ObservableParkingSpace> ParkingSpaces
        {
            get
            {
                return this._parkingSpaces;
            }
            set
            {
                if (value != this._parkingSpaces)
                {
                    this.Set(ref this._parkingSpaces, value);
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                if (value != this._isBusy)
                {
                    this.Set(ref this._isBusy, value);
                }
            }
        }

        public RelayCommand ParkACarCmd
        {
            get
            {
                return this._parkACarCmd ?? (this._parkACarCmd =
                    new RelayCommand(this.ParkACarAction));
            }
            set
            {
                if (value != this._parkACarCmd)
                    this.Set(ref this._parkACarCmd, value);
            }
        }

        public RelayCommand ParkABikeCmd
        {
            get
            {
                return this._parkABikeCmd ?? (this._parkABikeCmd =
                    new RelayCommand(this.ParkABikeAction));
            }
            set
            {
                if (value != this._parkABikeCmd)
                    this.Set(ref this._parkACarCmd, value);
            }
        }

        public int NbCarsAvailable => this.ParkingSpaces.Where(x => x.Type == "Voiture" && x.IsAvailable).Count();

        public int NbBikeAvailable => this.ParkingSpaces.Where(x => x.Type == "Moto" && x.IsAvailable).Count();

        public RelayCommand<ObservableParkingSpace> LeavingVehicleCmd
        {
            get
            {
                return this._leavingVehicleCmd ?? (this._leavingVehicleCmd =
                    new RelayCommand<ObservableParkingSpace>(this.LeavingVehicleAction));
            }
            set
            {
                if (value != this._leavingVehicleCmd)
                    this.Set(ref this._leavingVehicleCmd, value);
            }
        }

        #endregion

        #region "Actions"

        private void ParkACarAction()
        {
            this.ParkVehiculeAsync("Voiture");
        }

        private void ParkABikeAction()
        {
            this.ParkVehiculeAsync("Moto");
        }

        private void LeavingVehicleAction(ObservableParkingSpace space)
        {
            this.IsBusy = true;

            Task.Run(() => this._parkingService.EmptyParkingSpace(space)).ContinueWith((unused) =>
            {
                this.IsBusy = false;
                this.RaisePropertyChanged(() => this.NbBikeAvailable);
                this.RaisePropertyChanged(() => this.NbCarsAvailable);
            });
        }

        #endregion

        private Task ParkVehiculeAsync(string vehiculeType)
        {
            this.IsBusy = true;

            return Task.Run(() => this.ParkVehicule(vehiculeType)).ContinueWith((unused) =>
            {
                this.IsBusy = false;
                this.RaisePropertyChanged(() => this.NbBikeAvailable);
                this.RaisePropertyChanged(() => this.NbCarsAvailable);
            });
        }

        private void ParkVehicule(string vehiculeType)
        {
            ObservableVehicule vehicule;
            try
            {
                vehicule = this._vehiculeService.GetNewVehicule(vehiculeType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Impossible de créer une voiture");
                return;
            }

            try
            {
                this._parkingService.ParkAVehicule(this.ParkingSpaces, vehicule);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Impossible de garer une voiture");
                return;
            }
        }

        private Task GetParkingSpacesAsync()
        {
            this.IsBusy = true;

            return Task.Run(() => this.ParkingSpaces = this._parkingService.GetParkingSpaces()).
                                                           ContinueWith((unused) => this.IsBusy = false);
        }

        public override void Cleanup()
        {
            Broker.Unsubscribe(this);
            base.Cleanup();
        }
    }
}