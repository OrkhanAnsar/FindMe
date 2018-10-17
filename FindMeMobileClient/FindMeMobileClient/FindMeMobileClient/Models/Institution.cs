using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FindMeMobileClient.Models {
    public class Institution : BindableObject {
        private int _id;
        private string _name;
        private string _phone;
        private string _address;
        private string _openingHours;
        private string _website;
        private int _cityId;
        private float longitude;
        private float latitude;

        public float Latitude {
            get { return latitude; }
            set { latitude = value; base.OnPropertyChanged(); }
        }


        public float Longitude {
            get { return longitude; }
            set { longitude = value; base.OnPropertyChanged(); }
        }


        private bool isVisible;

        public bool IsVisible {
            get { return isVisible; }
            set { isVisible = value; base.OnPropertyChanged(); }
        }


        public int Id { get => _id; set { _id = value; base.OnPropertyChanged(); } }
        public int CityId { get => _cityId; set { _cityId = value; base.OnPropertyChanged(); } }
        public string Name {
            get => _name; set { _name = value; base.OnPropertyChanged(); }
        }
        public string Address { get => _address; set { _address = value; base.OnPropertyChanged(); } }
        public string Phone { get => _phone; set { _phone = value; base.OnPropertyChanged(); } }
        public string OpeningHours { get => _openingHours; set { _openingHours = value; base.OnPropertyChanged(); } }
        public string Website { get => _website; set {_website = value; base.OnPropertyChanged(); } }

        public City City { get; set; }
    }
}
