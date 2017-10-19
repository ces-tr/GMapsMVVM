using Autofac;
using Autofac.Core;
using Autofac.Extras.CommonServiceLocator;
using GmapsCore.Utils;
using GmapsCore.ViewModel;
using Infrastructure.Common;
using Microsoft.Practices.ServiceLocation;
using Plugin.Geolocator.Abstractions;

public class ViewModelLocator
{
    static ViewModelLocator()
    {
        //ContainerBuilder containerBuilder = new ContainerBuilder();

        //builder.RegisterType<SettingsService>().As<ISettingsService>();
        //builder.RegisterType<DbConnectionService>().As<IDbConnectionService>();
        //builder.RegisterType<LocalDataService>().As<ILocalDataService>();
        //builder.RegisterType<LocalFilesService>().As<ILocalFilesService>();

        //builder.RegisterType<SocialService>().As<ISocialService>();
        //builder.RegisterType<Services.JsonService>().As<Contracts.IJsonService>();
        //builder.RegisterType<StoreService>().As<IStoreService>();
        //builder.RegisterType<NavigationService>().As<INavigationService>();
        //builder.RegisterType<NetworkService>().As<INetworkService>();
        //builder.RegisterType<HttpClientService>().As<IHttpClientService>();

        //containerBuilder.RegisterType<MainViewModel>().As<MainViewModel>().SingleInstance();
        //builder.RegisterType<PersonViewModel>().As<PersonViewModel>().SingleInstance();
        //builder.RegisterType<AboutViewModel>().As<AboutViewModel>().SingleInstance();

        //container = containerBuilder.Build(Autofac.Builder.ContainerBuildOptions.None);

        //AppContainer.Container = SetupContainer();

    }

    public static void Init(IModule[] platformSpecificModules)
    {
        AppContainer.Container = SetupContainer(platformSpecificModules);
    }

    static IContainer SetupContainer(IModule[] platformSpecificModules )
	{
		ContainerBuilder containerBuilder = new ContainerBuilder();

        RegisterPlatformSpecificModules(platformSpecificModules, containerBuilder);

		RegisterDependencies(containerBuilder);

		return containerBuilder.Build(Autofac.Builder.ContainerBuildOptions.None);
	}

	static void RegisterPlatformSpecificModules(IModule[] platformSpecificModules, ContainerBuilder containerBuilder)
	{
		foreach (var platformSpecificModule in platformSpecificModules)
		{
			containerBuilder.RegisterModule(platformSpecificModule);
		}
	}

	static void RegisterDependencies(ContainerBuilder cb)
	{
		//cb.RegisterType<HomeViewModel>().SingleInstance();
        cb.RegisterType<MainViewModel>().As<MainViewModel>().SingleInstance();
	}

    public static MainViewModel Main
    {
        get
        {
            return AppContainer.Container.Resolve<MainViewModel>();
        }
    }

}