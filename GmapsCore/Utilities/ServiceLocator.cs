using System;
using Autofac;
using GmapsCore.Utils;
using Plugin.Geolocator.Abstractions;

namespace GmapsCore.Utilities
{
    public class ServiceLocator
    {
        public static IGeolocator GeolocatorService
		{
            get
			{
				return AppContainer.Container.Resolve<IGeolocator>();
			}

		}

    }
}
