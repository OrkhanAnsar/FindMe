using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FindMePrism.Helpers;

namespace FindMePrism.Models
{
    public class Institution : BindableBase, ICloneable, IDataErrorInfo
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private string login;
        private string password;
        private string openingHours;
        private string website;
        private City institutionCity;
        private int cityId;
        private bool isAdmin;
        private int institutionTypeId;
        private InstitutionType institutionType;
        private float latitude;
        private float longitude;
        private string newPassword;
        private string confirmPassword;

        public int Id { get => id; set { id = value; base.RaisePropertyChanged(); } }
        //[Required(ErrorMessage = "This field is required")]
        public string Name { get => name; set { name = value; base.RaisePropertyChanged(); } }
        public string Address { get => address; set { address = value; base.RaisePropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; base.RaisePropertyChanged(); } }
        public string Login { get => login; set { login = value; base.RaisePropertyChanged(); } }
        public string Password { get => password; set { password = value; base.RaisePropertyChanged(); } }
        public string OpeningHours { get => openingHours; set { openingHours = value; base.RaisePropertyChanged(); } }
        public string Website { get => website; set { website = value; base.RaisePropertyChanged(); } }
        public City City { get => institutionCity; set { institutionCity = value; base.RaisePropertyChanged(); } }
        public int CityId { get => cityId; set { cityId = value; base.RaisePropertyChanged(); } }
        public bool IsAdmin { get => isAdmin; set { isAdmin = value; base.RaisePropertyChanged(); } }
        public int InstitutionTypeId { get => institutionTypeId; set { institutionTypeId = value; base.RaisePropertyChanged(); } }
        public InstitutionType InstitutionType { get => institutionType; set { institutionType = value; base.RaisePropertyChanged(); } }
        public float Latitude { get => latitude; set { latitude = value; base.RaisePropertyChanged(); } }
        public float Longitude { get => longitude; set { longitude = value; base.RaisePropertyChanged(); } }
        public string NewPassword { get => newPassword; set { newPassword = value; base.RaisePropertyChanged(); } }
        [JsonIgnore]
        public string ConfirmPassword { get => confirmPassword; set { confirmPassword = value; base.RaisePropertyChanged(); } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string Error { get => String.Empty; }
        public string this[string columnName] => this.Validate(columnName);
    }
}