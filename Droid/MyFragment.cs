
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using GalaSoft.MvvmLight.Helpers;
using GMapsMVVM.Droid;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using GmapsCore.ViewModel;

namespace GMapsMVVM.Droid
{
	public class MyFragment : Android.Support.V4.App.Fragment , IOnMapReadyCallback
	{
		GoogleMap GMap;
        MapView GMapView;
        Marker marker;
		private readonly List<Binding> bindings = new List<Binding>();
        public bool tracking { get; private set; }

        MainViewModel MapViewViewModel => ViewModelLocator.Main;

		public async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
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

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment

			View rootView = inflater.Inflate(Resource.Layout.MapViewFragment, container, false);

			return rootView;

			//return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public async void OnMapReady(GoogleMap googleMap)
		{
			this.GMap = googleMap;

			this.GMap.UiSettings.ZoomControlsEnabled = true;
			LatLng latlng = new LatLng(Convert.ToDouble(37.797865), Convert.ToDouble(-122.402526));
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
			GMap.MoveCamera(camera);
			MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle("Imhere");
            options.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.car_top));

            marker = GMap.AddMarker(options);
			
            bindings.Add(this.SetBinding(
				 () => MapViewViewModel.Position,
				 () => marker.Position).ConvertSourceToTarget(locationProperty =>
				 {
					 return (locationProperty == null) ? new LatLng(37.797865, -122.402526) : new LatLng(locationProperty.Latitude, locationProperty.Longitude);
				 }
                                                             ));

			

		}

        public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);
			FragmentActivity frameActivity = Activity;

            GMapView = (MapView)view.FindViewById(Resource.Id.map);
			GMapView.OnCreate(savedInstanceState);
			GMapView.OnResume();
			GMapView.GetMapAsync(this);

		}


		//private void BindViewHolder(CachingViewHolder holder, TaskModel taskModel, int position)
		//{
		//	// if the data source doesn't change use the simpler form below
		//	//var name = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
		//	//name.Text = taskModel.Name;

		//	//var desc = holder.FindCachedViewById<TextView>(Resource.Id.NotesTextView);
		//	//desc.Text = taskModel.Notes;

		//	var name = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
		//	holder.DeleteBinding(name);

		//	var nameBinding = new Binding<string, string>(taskModel,
		//												  nameof(taskModel.Name),
		//												  name,
		//												  nameof(name.Text),
		//												  BindingMode.OneWay);

		//	holder.SaveBinding(name, nameBinding);

		//	var desc = holder.FindCachedViewById<TextView>(Resource.Id.NotesTextView);
		//	holder.DeleteBinding(desc);

		//	var descBinding = new Binding<string, string>(taskModel,
		//												  nameof(taskModel.Notes),
		//												  desc,
		//												  nameof(desc.Text),
		//												  BindingMode.OneWay);

		//	holder.SaveBinding(desc, descBinding);
		//}


	}
}
