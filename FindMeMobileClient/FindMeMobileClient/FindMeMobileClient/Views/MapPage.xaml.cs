using FindMeMobileClient.Models;
using FindMeMobileClient.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FindMeMobileClient.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage {
        public MapPage() {
            InitializeComponent();
            MessagingCenter.Subscribe<MorePageViewModel, Lost>(this, "GetLocation", (s, l) => {
                this.SetLocation(new Position(l.Institution.Latitude, l.Institution.Longitude), l.Institution.Name);
                this.Title = l.Institution.Name;
            });
            MessagingCenter.Subscribe<OrganizationsPageViewModel, Institution>(this, "GetLocation", (s, i) => {
                this.SetLocation(new Position(i.Latitude, i.Longitude), i.Name);
                this.Title = i.Name;
            });
        }

        public void SetLocation(Position position, string label) {
            this.map.Pins.Clear();
            this.map.Pins.Add(new Pin {
                Label = label,
                Position = position
            });
            this.map.MoveToRegion(new MapSpan(map.Pins[0].Position, 0.1, 0.1));
        }
    }
}