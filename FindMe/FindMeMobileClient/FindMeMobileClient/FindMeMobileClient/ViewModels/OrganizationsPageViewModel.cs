using FindMeMobileClient.Models;
using FindMeMobileClient.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FindMeMobileClient.ViewModels
{
    public class OrganizationsPageViewModel : BindableBase
    {
        private readonly IDataService dataService;
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;

        public OrganizationsPageViewModel(INavigationService navigationService, IDataService dataService, IPageDialogService pageDialogService) 
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.pageDialogService = pageDialogService;
            this.IsBusy = false;
            this.Institutions = new ObservableCollection<Institution>();
            this.RefreshInstitutionsCommand = new DelegateCommand(Update);
            this.ShowOnMapCommand = new DelegateCommand(ShowOnMapMethod);
            Update();
        }

        #region IsBusy
        private bool isBusy;
        public bool IsBusy { get => this.isBusy; set { SetProperty(ref this.isBusy, value); } }
        #endregion

        private Institution selectedItem;

        public Institution SelectedItem {
            get { return selectedItem; }
            set {
                SetProperty(ref this.selectedItem, value);
            }
        }

        public DelegateCommand ShowOnMapCommand { get; set; }

        public async void ShowOnMapMethod() {
            var prms = new NavigationParameters();
            prms.Add("OrganizationsPageQuery", SelectedItem);
            await navigationService.NavigateAsync("MapPage", prms);
            MessagingCenter.Send(this, "GetLocation", SelectedItem);
        }

        public ObservableCollection<Institution> Institutions { get; set; }

        public DelegateCommand RefreshInstitutionsCommand { get; set; }

        public async void Update()
        {
            this.IsBusy = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Institutions.Clear();
            });
            var institutions = await this.dataService.GetInstitutions();
            if (institutions != null) {
                foreach (var item in institutions) {
                    Device.BeginInvokeOnMainThread(() => {
                        this.Institutions.Add(item);
                    });
                }
            } else {
                //await this.navigationService.NavigateAsync("app:///MainPage/NavigationPage/OfflinePage");
                if (await this.pageDialogService.DisplayAlertAsync("Connection error", "No internet connection, try later", "Update", "Cancel")) {
                    Update();
                }
            }
            this.IsBusy = false;
        }
    }
}
