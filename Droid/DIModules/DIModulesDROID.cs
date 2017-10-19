using System;
using Autofac;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace GMapsMVVM.Droid.DIModules
{
    public class DIModulesDROID : Module
    {
        protected override void Load(ContainerBuilder builder){

			base.Load(builder);
			builder.RegisterType<GeolocatorImplementationDROID>().As<IGeolocator>().SingleInstance();
		}
    }
}