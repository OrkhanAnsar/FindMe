using FindMePrism.Events;
using FindMePrism.Services;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace FindMePrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public IRegionManager regionManager;
        public IEventAggregator eventAggregator { get; }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public InteractionRequest<Notification> ShowNotificationInteractionRequest { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IAuthenticationService authService, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.ShowNotificationInteractionRequest = new InteractionRequest<Notification>();
            this.eventAggregator.GetEvent<ShowAlertEvent>().Subscribe((n)=> {
                this.ShowNotificationInteractionRequest.Raise(n);
            });
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
