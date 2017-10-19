using System;
using GmapsCore.ViewModel;
using Plugin.Geolocator.Abstractions;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;

#if __UNIFIED__
using UIKit;
using CoreLocation;
using CoreGraphics;

#else
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreLocation;

// Type Mappings Unified to monotouch.dll
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;

using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

using Google.Maps;

namespace GMapsMVVM.iOS
{
	public class MapViewController : UIViewController
	{
		MapView mapView;
        MainViewModel MapViewViewModel => ViewModelLocator.Main;
        Marker xamMarker;
        bool tracking;

        private readonly List<Binding> bindings = new List<Binding>();

		public async override void LoadView ()
		{
			base.LoadView ();

			if (MapViewViewModel.LocationService.IsListening)
			{
				await MapViewViewModel.LocationService.StopListeningAsync();
                tracking = false;
				
			}
			else
			{
				//Positions.Clear();
				if (await MapViewViewModel.LocationService.StartListeningAsync(TimeSpan.FromSeconds(1), 50))
				{
					//labelGPSTrack.Text = "Started tracking";
					//ButtonTrack.Text = "Stop Tracking";
					tracking = true;
				}
			}

            CameraPosition camera = CameraPosition.FromCamera (37.797865, -122.402526, 10);

			mapView = MapView.FromCamera (CGRect.Empty, camera);
			mapView.MyLocationEnabled = true;

			xamMarker = new Marker () {
				Title = "Xamarin HQ",
				Snippet = "Where the magic happens.",
                Icon = UIImage.FromBundle("car.png"),
                Map = mapView
			};

			mapView.SelectedMarker = xamMarker;

            View = mapView;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			bindings.Add(this.SetBinding(
				 () => MapViewViewModel.Position,
				 () => xamMarker.Position).ConvertSourceToTarget(locationProperty =>
                 {
                     return (locationProperty == null) ? new CLLocationCoordinate2D(37.797865, -122.402526) : new CLLocationCoordinate2D(locationProperty.Latitude, locationProperty.Longitude);
                 }
            ));
		}

		public override void ViewWillDisappear (bool animated)
		{	
			base.ViewWillDisappear (animated);
		}
	}
}

