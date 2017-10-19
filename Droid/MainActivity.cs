using Android.App;
using Android.Widget;
using Android.OS;
using Autofac.Core;
using GMapsMVVM.Droid.DIModules;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using GalaSoft.MvvmLight.Helpers;
using GmapsCore.ViewModel;
using Android.Support.V7.App;
using SupportV4 = Android.Support.V4;


namespace GMapsMVVM.Droid
{
    [Activity(Label = "GMapsMVVM", MainLauncher = true, Theme = "@style/AppTheme",Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity 
    {
        int count = 1;


		const string _FRAG1_TAG = "MAPFRAGMENT_TAG";
		
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModelLocator.Init(new IModule[] { new DIModulesDROID() });

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
			SupportV4.App.FragmentManager fragMgr = this.SupportFragmentManager;
			var xact = fragMgr.BeginTransaction();


			if (null == fragMgr.FindFragmentByTag(_FRAG1_TAG))
			{
				var fragment = new MyFragment();

				xact.Add(Android.Resource.Id.Content, fragment, _FRAG1_TAG);

			}
			xact.CommitAllowingStateLoss();

			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.tasksToolbar);
			//Toolbar will now take on default Action Bar characteristics
			//SetActionBar (toolbar);
			SetSupportActionBar(toolbar);


           
            //SetUpMap();
        }

		//private void SetUpMap()
		//{
		//	if (GMap == null)
		//	{
		//		FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
		//	}
		//}

		
    }
}

