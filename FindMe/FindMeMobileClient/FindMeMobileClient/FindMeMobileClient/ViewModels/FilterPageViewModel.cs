using FindMeMobileClient.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindMeMobileClient.ViewModels {
    public class FilterPageViewModel : BindableBase {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        public FilterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            ApplyCommand = new DelegateCommand(Apply);
            if (App.Filter != null)
            {
                var localFilter = App.Filter;
                this.AgeBegin = localFilter.AgeBegin.ToString();
                this.AgeEnd = localFilter.AgeEnd.ToString();
                this.FirstName = localFilter.FirstName;
                this.MiddleName = localFilter.MiddleName;
                this.LastName = localFilter.LastName;
                this.Height = localFilter.Height.ToString();
                this.SelectedHairColor = localFilter.HairColor;
                this.SelectedEyeColor = localFilter.EyeColor;
                this.SelectedBodyType = localFilter.BodyType;
                this.SelectedGender = localFilter.Gender;
            }
        }

        public DelegateCommand ApplyCommand { get; set; }

        public void Apply() {
            int ageBegin = int.Parse(string.IsNullOrWhiteSpace(this.AgeBegin) ? "0" : this.AgeBegin);
            if (ageBegin < 0 || ageBegin > 150) { pageDialogService.DisplayAlertAsync("filter message", "Enter filter info correct please", "OK"); return; }
            int ageEnd = int.Parse(string.IsNullOrWhiteSpace(this.AgeEnd) ? "0" : this.AgeEnd);
            if (ageEnd < ageBegin || ageEnd > 150) { pageDialogService.DisplayAlertAsync("filter message", "Enter filter info correct please", "OK"); return; }
            App.Filter = new Filter {
                FirstName = string.IsNullOrWhiteSpace(this.FirstName) ? null : this.FirstName,
                MiddleName = string.IsNullOrWhiteSpace(this.MiddleName) ? null : this.MiddleName,
                LastName = string.IsNullOrWhiteSpace(this.LastName) ? null : this.LastName,
                AgeBegin = ageBegin,
                AgeEnd = ageEnd,
                BodyType = this.SelectedBodyType,
                EyeColor = this.SelectedEyeColor,
                HairColor = this.SelectedHairColor,
                Gender = this.SelectedGender,
                Height = int.Parse(string.IsNullOrWhiteSpace(this.Height) ? "0" : this.Height),
                FilterDate = DateTime.Now
            };
            var param = new NavigationParameters();
            param.Add("filter", true);
            navigationService.GoBackAsync(param);
        }

        private string firstName;
        public string FirstName {
            get { return firstName; }
            set { base.SetProperty(ref this.firstName, value); }
        }

        private string middleName;
        public string MiddleName {
            get { return middleName; }
            set { base.SetProperty(ref this.middleName, value); }
        }

        private string lastName;
        public string LastName {
            get { return lastName; }
            set { base.SetProperty(ref this.lastName, value); }
        }

        private string ageBegin;

        public string AgeBegin {
            get { return ageBegin; }
            set { base.SetProperty(ref this.ageBegin, value); }
        }

        private string ageEnd;

        public string AgeEnd {
            get { return ageEnd; }
            set { base.SetProperty(ref this.ageEnd, value); }
        }

        private string height;

        public string Height {
            get { return height; }
            set { base.SetProperty(ref this.height, value); }
        }

        private string selectedHairColor;

        public string SelectedHairColor {
            get { return selectedHairColor; }
            set { base.SetProperty(ref this.selectedHairColor, value); }
        }

        private string selectedEyeColor;

        public string SelectedEyeColor {
            get { return selectedEyeColor; }
            set { base.SetProperty(ref this.selectedEyeColor, value); }
        }

        private string selectedBodyType;

        public string SelectedBodyType {
            get { return selectedBodyType; }
            set { base.SetProperty(ref this.selectedBodyType, value); }
        }

        private string selectedGender;

        public string SelectedGender {
            get { return selectedGender; }
            set { base.SetProperty(ref this.selectedGender, value); }
        }
    }
}
