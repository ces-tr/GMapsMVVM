using System;
using Autofac;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace GMapsMVVM.iOS.DIModules
{
    public class DIModulesIOS : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
			builder.RegisterType<GeolocatorImplementationIOS>().As<IGeolocator>().SingleInstance();
			//builder.RegisterType<SqlitePlatformProvider>().As<ISqlitePlatformProvider>();
        }
    }
}
