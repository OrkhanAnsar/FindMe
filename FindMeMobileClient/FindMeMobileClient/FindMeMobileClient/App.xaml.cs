using FindMeMobileClient.Models;
using FindMeMobileClient.Services;
using FindMeMobileClient.Services.Interfaces;
using FindMeMobileClient.ViewModels;
using FindMeMobileClient.Views;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FindMeMobileClient
{
    public partial class App : PrismApplication
	{
        public static int Schitalka { get; set; } = 0;
        public static Filter Filter { get; set; }
        public static string Token;
        public const string NotificationReceivedKey = "NotificationReceived";
        public const string MobileServiceUrl = "https://findmeazserver.azurewebsites.net";
        public App() : base(null)
        {
            InitializeComponent();
        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.Register<ISubscribeService, SubscribeService>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<OrganizationsPage>();
            containerRegistry.RegisterForNavigation<FilterPage>();
            containerRegistry.RegisterForNavigation<MorePage>();
            containerRegistry.RegisterForNavigation<SubscribesPage>();
            containerRegistry.RegisterForNavigation<MapPage>();
        }

        protected override void OnInitialized()
        {
            this.NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void OnStart ()
		{

        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
    }
}