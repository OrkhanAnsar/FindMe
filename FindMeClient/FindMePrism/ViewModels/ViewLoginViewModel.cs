using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.ComponentModel;

namespace FindMePrism.ViewModels
{
    public class ViewLoginViewModel : BindableBase
    {
        private Institution institution;
        public Institution Institution
        {
            get { return institution; }
            set { SetProperty(ref institution, value); }
        }

        public DelegateCommand LoginCommand { get; set; }
        public IEventAggregator eventAggregator { get; }
        private readonly IRegionManager regionManager;
        private readonly IAuthenticationService authService;
        private readonly ILostService lostService;
        private readonly IInstitutionService institutionService;

        public ViewLoginViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IAuthenticationService authService, ILostService lostService, IInstitutionService institutionService)
        {
            this.Institution = new Institution();
            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute);
            Institution.PropertyChanged += Institution_PropertyChanged;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.authService = authService;
            this.lostService = lostService;
            this.institutionService = institutionService;
        }

        private void Institution_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.LoginCommand.RaiseCanExecuteChanged();
        }


        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }

        private async void LoginCommandExecute()
        {
            this.Institution.Login.ToLower();
            var user = await this.authService.Validate(this.Institution);
            if (user != null)
            {
                if (user.IsAdmin)  {
                    var insts = await this.institutionService.GetInstitutions();
                    Navigate("ViewAdmin");
                    if (insts != null)
                        this.eventAggregator.GetEvent<InstsEvent>().Publish(insts);
                }
                else {
                    var losts = await this.lostService.GetLosts(user);
                    Navigate("ViewLosts");
                    if (losts != null)
                        this.eventAggregator.GetEvent<LostsEvent>().Publish(losts);
                    this.eventAggregator.GetEvent<InstEvent>().Publish(user);
                }
                Institution.PropertyChanged -= Institution_PropertyChanged;
                Institution = new Institution();
                Institution.PropertyChanged += Institution_PropertyChanged;
                Institution.IsAdmin = false;

            }
            else {
                this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "This user does not exist!", Title = "Error" });
            }
        }

        private bool LoginCommandCanExecute()
        {
            return !String.IsNullOrWhiteSpace(Institution?.Login) & !String.IsNullOrWhiteSpace(Institution?.Password);
        }
    }
}
