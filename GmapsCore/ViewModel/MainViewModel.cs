using GalaSoft.MvvmLight;
using Infrastructure.Common;
using Plugin.Geolocator.Abstractions;

namespace GmapsCore.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public IGeolocator LocationService { get; set; }

        Position _Position;
		public Position Position
		{
			get
			{
				return _Position;
			}
			set
			{
                Set<Position>(ref _Position, value);
                //_Position = value;
                //RaisePropertyChanged(()=> Position);
			}
		}

        double _Latitude;
        public double Latitude
        {
            get{
                return _Latitude;
            }
            set{
                Set<double>(ref _Latitude, value);
            }
        }


		double _Longitud;
		public double Longitud
		{
			get
			{
				return _Longitud;
			}
			set
			{
				Set<double>(ref _Longitud, value);
			}
		}

		private string name = string.Empty;
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				Set<string>(ref name, value);
			}
		}

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public MainViewModel(IGeolocator locationService )
        {
            LocationService = locationService;
            LocationService.PositionChanged += (sender, e) => {

                Latitude = e.Position.Latitude;
                Longitud = e.Position.Longitude;
                Position = e.Position;
            };
        }
    }
}