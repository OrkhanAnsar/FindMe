using System;
using System.ComponentModel;
using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace FindMePrism.ViewModels
{
    public class ViewChangePasswordViewModel : BindableBase
    {
        private Institution institution;
        public Institution Institution
        {
            get { return institution; }
            set { SetProperty(ref institution, value); }
        }

        public DelegateCommand SaveCommand { get; set; }
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly IInstitutionService institutionService;


        public ViewChangePasswordViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IInstitutionService institutionService)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.institutionService = institutionService;
            this.Institution = new Institution();
            SaveCommand = new DelegateCommand(SaveCommandExecute, SaveCommandCanExecute);
            this.eventAggregator.GetEvent<ChangePasswordEvent>().Subscribe(GetInstitution);
        }

        private void Institution_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void GetInstitution(Institution institution)
        {
            
            if (institution != null) {
                this.Institution = institution.Clone() as Institution;
                this.Institution.PropertyChanged += Institution_PropertyChanged;
            }
        }

        private DelegateCommand cancelCommand;
        public DelegateCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new DelegateCommand(
                    () => {
                        Navigate("ViewAdmin");
                        Institution = new Institution();
                    }
                ));
            }
        }


        private async void SaveCommandExecute()
        {
            var res = await institutionService.ChangePassword(Institution);
            if (res)
            {
                Navigate("ViewAdmin");
                Institution = new Institution();
            }
            else
                this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "Error", Title = "Error" });

        }


        private bool SaveCommandCanExecute()
        {
             if (String.IsNullOrWhiteSpace(Institution?.NewPassword) || String.IsNullOrWhiteSpace(Institution?.ConfirmPassword) || String.IsNullOrWhiteSpace(Institution?.Password))
                 return false;
             else if ((Institution?.NewPassword == Institution?.ConfirmPassword) & (Institution?.NewPassword.Length >= 6))
                    return true;
             else
                 return false;
        }
    }
}
