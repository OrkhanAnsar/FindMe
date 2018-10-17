using FindMeMobileClient.Models;
using FindMeMobileClient.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FindMeMobileClient.ViewModels {
    public class MapPageViewModel : BindableBase, INavigationAware {

        public MapPageViewModel() {
            this.PinPosition = new Position();
        }

        public string Name { get; set; }

        private string address;

        public string Address {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private Position pinPosition;

        public Position PinPosition {
            get { return pinPosition; }
            set { SetProperty(ref this.pinPosition, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters) {
            if (parameters.ContainsKey("MorePageQuery")) {
                if (parameters["MorePageQuery"] is Lost lost) {
                    // потом поставь если асинхронно будешь делать device....
                    this.Name = lost.Institution.Name;
                    this.Address = lost.Institution.Address;
                    this.PinPosition = new Position(lost.Institution.Latitude, lost.Institution.Longitude);
                    MessagingCenter.Subscribe<MapPage>(this, "GetLocation", (e) => {
                        e.SetLocation(this.PinPosition, this.Address);
                    });
                }
            } else if (parameters.ContainsKey("OrganizationsPageQuery")) {
                if (parameters["OrganizationsPageQuery"] is Institution institution) {
                    this.Name = institution.Name;
                    this.PinPosition = new Position(institution.Latitude,
                        institution.Longitude);
                    MessagingCenter.Subscribe<MapPage>(this, "GetLocation", (e) => {
                        e.SetLocation(this.PinPosition, this.Address);
                    });
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
