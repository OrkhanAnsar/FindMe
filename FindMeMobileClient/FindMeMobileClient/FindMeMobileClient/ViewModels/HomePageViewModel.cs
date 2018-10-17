using FindMeMobileClient.Models;
using FindMeMobileClient.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace FindMeMobileClient.ViewModels {
    public class HomePageViewModel : BindableBase, INavigationAware {
        private readonly IPageDialogService pageDialogService;
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        public HomePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDataService dataService) {
            this.pageDialogService = pageDialogService;
            this.dataService = dataService;
            this.navigationService = navigationService;
            this.IsBusy = false;
            this.SearchCommand = new DelegateCommand(Search);
            this.FilterCommand = new DelegateCommand(Filter);
            this.MoreCommand = new DelegateCommand(More);
            this.RefreshLostsCommand = new DelegateCommand(Update);
            this.Losts = new ObservableCollection<Lost>();
        }

        public ObservableCollection<Lost> Losts { get; set; }

        #region IsBusy
        private bool isBusy;
        public bool IsBusy { get => this.isBusy; set { SetProperty(ref this.isBusy, value); } }
        #endregion

        #region Search
        public DelegateCommand SearchCommand { get; set; }
        private string searchText;
        public string SearchText {
            get { return searchText; }
            set {
                SetProperty(ref this.searchText, value);
            }
        }
        public void Search() {
            Update(this.SearchText);
        }
        #endregion

        #region Filter
        public DelegateCommand FilterCommand { get; set; }

        public void Filter() {
            navigationService.NavigateAsync("FilterPage");
        }
        #endregion

        #region More

        public DelegateCommand RefreshLostsCommand { get; set; }

        private Lost selectedItem;

        public Lost SelectedItem {
            get { return selectedItem; }
            set {
                SetProperty(ref this.selectedItem, value);
            }
        }

        public DelegateCommand MoreCommand { get; set; }

        public async void More() {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("SelectedLost", SelectedItem);
            await navigationService.NavigateAsync("MorePage", navigationParameters);
        }
        #endregion

        public async void Update() {
            this.IsBusy = true;
            var losts = await this.dataService.GetLosts();
            this.Losts.Clear();
            if (losts != null) {
                if (App.Filter == null) {
                    {
                        foreach (var item in losts) {
                            this.Losts.Add(item);
                        }
                    }
                } else {
                    var lostsFiltered = losts.Where(p => string.IsNullOrWhiteSpace(p.FirstName) ? true : string.IsNullOrWhiteSpace(App.Filter.FirstName) ? true : (p.FirstName == App.Filter.FirstName) &&
                            string.IsNullOrWhiteSpace(p.MiddleName) ? true : string.IsNullOrWhiteSpace(App.Filter.MiddleName) ? true : (p.MiddleName == App.Filter.MiddleName) &&
                            string.IsNullOrWhiteSpace(p.LastName) ? true : string.IsNullOrWhiteSpace(App.Filter.LastName) ? true : (p.LastName == App.Filter.LastName) &&
                            string.IsNullOrWhiteSpace(p.Age) ? true : App.Filter.AgeBegin == 0 && App.Filter.AgeEnd == 0 ? true :
                            App.Filter.AgeBegin < p.AgeBegin && p.AgeBegin < App.Filter.AgeEnd || App.Filter.AgeBegin < p.AgeEnd && p.AgeEnd < App.Filter.AgeEnd ||
                            p.AgeBegin < App.Filter.AgeBegin && p.AgeEnd > App.Filter.AgeEnd &&
                            App.Filter.Height == 0 ? true : App.Filter.Height == p.Height &&
                            string.IsNullOrEmpty(App.Filter.HairColor) ? true : p.HairColor == App.Filter.HairColor &&
                            string.IsNullOrEmpty(App.Filter.EyeColor) ? true : p.EyeColor == App.Filter.EyeColor &&
                            string.IsNullOrEmpty(App.Filter.BodyType) ? true : p.BodyType == App.Filter.BodyType &&
                            string.IsNullOrEmpty(App.Filter.Gender) ? true : p.Gender == App.Filter.Gender);
                    foreach (var item in lostsFiltered) {
                        this.Losts.Add(item);
                    }
                }
            } else {
                //await this.navigationService.NavigateAsync("app:///MainPage/NavigationPage/OfflinePage");
                if (await pageDialogService.DisplayAlertAsync("Connection error", "No internet connection, try later", "Update", "Cancel")) {
                    Update();
                }
            }
            this.IsBusy = false;
        }

        public async void Update(string param) {
            var losts = await this.dataService.GetLosts();
            if (losts != null) {
                var lostsFiltered = losts.Where((p) => p.FullName.ToLower().Contains(param.ToLower()));
                this.Losts.Clear();
                foreach (var item in lostsFiltered) {
                    this.Losts.Add(item);
                }
            } else {
                if (await this.pageDialogService.DisplayAlertAsync("Connection error", "No internet connection, try later", "Update", "Cancel")) {
                    Update(param);
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters) {
            if (string.IsNullOrWhiteSpace(SearchText)) {
                Update();
            } else {
                Update(SearchText);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}