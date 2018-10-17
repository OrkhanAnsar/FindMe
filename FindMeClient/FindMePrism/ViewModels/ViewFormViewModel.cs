using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.ComponentModel;

namespace FindMePrism.ViewModels
{
    public class ViewFormViewModel : BindableBase
    {
        private Lost lost;
        public Lost Lost { get => lost; set => SetProperty(ref lost, value); }

        private Institution institution;
        public Institution Institution { get => institution; set => SetProperty(ref institution, value); }

        private List<string> hairColors;
        public List<string> HairColors { get => hairColors; set => SetProperty(ref hairColors, value); }

        private List<string> eyeColors;
        public List<string> EyeColors { get => eyeColors; set => SetProperty(ref eyeColors, value); }

        private List<string> genders;
        public List<string> Genders { get => genders; set => SetProperty(ref genders, value); }

        private List<string> bodyTypes;
        public List<string> BodyTypes { get => bodyTypes; set => SetProperty(ref bodyTypes, value); }

        private string label;
        public string Label { get => label; set => SetProperty(ref label, value); }

        public IEventAggregator eventAggregator { get; }
        public IRegionManager regionManager { get; }
        public ILostService lostService { get; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand FillFieldsCommand { get; }
        public bool editProcess { get; set; }

        public void FillEyeColors()
        {
            EyeColors = new List<string>();
            var ec = this.lostService.GetEyeColors();
            foreach (var item in ec)
            {
                EyeColors.Add(item);
            }
        }
        public void FillHairColors()
        {
            HairColors = new List<string>();
            var hc = this.lostService.GetHairColors();
            foreach (var item in hc)
            {
                HairColors.Add(item);
            }
        }
        public void FillGenders()
        {
            Genders = new List<string>();
            var gs = this.lostService.GetGenders();
            foreach (var item in gs)
            {
                Genders.Add(item);
            }
        }
        public void FillBodyTypes()
        {
            BodyTypes = new List<string>();
            var bt = this.lostService.GetBodyTypes();
            foreach (var item in bt)
            {
                BodyTypes.Add(item);
            }
        }
        public void FillFields()
        {
            FillEyeColors();
            FillGenders();
            FillHairColors();
            FillBodyTypes();
        }

        public ViewFormViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILostService lostService)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<EditLostEvent>().Subscribe(GetLost);
            this.eventAggregator.GetEvent<InstEvent>().Subscribe(GetInstitution);
            this.regionManager = regionManager;
            this.lostService = lostService;
            this.Lost = new Lost();
            this.Lost.Institution = new Institution();
            this.Lost.Institution.City = new City();
            this.Lost.Institution.InstitutionType = new InstitutionType();
            this.Lost.DetectionTime = DateTime.Now;
            this.Lost.PropertyChanged += Lost_PropertyChanged;
            editProcess = false;
            FillFields();
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            FillFieldsCommand = new DelegateCommand(ExecuteFillFieldsCommand);
            Label = "Registration Form";
        }

        private void Lost_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        private void GetInstitution(Institution inst)
        {
            if (inst != null)
            {
                Institution = new Institution();
                Institution = inst.Clone() as Institution;
                Institution.City = inst.City.Clone() as City;
                Institution.InstitutionType = inst.InstitutionType.Clone() as InstitutionType;
            }
        }

        private void GetLost(Lost lost)
        {
            if (lost != null)
            {
                this.Lost.PropertyChanged -= Lost_PropertyChanged;
                this.Lost = lost.Clone() as Lost;
                this.Lost.Institution = lost.Institution.Clone() as Institution;
                this.Lost.Institution.InstitutionType = lost.Institution.InstitutionType.Clone() as InstitutionType;
                this.Lost.Institution.City = lost.Institution.City.Clone() as City;
                this.Lost.PropertyChanged += Lost_PropertyChanged;
                editProcess = true;
                Label = "Edit Form";
            }
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }

        private async void ExecuteSaveCommand()
        {
            if (editProcess)
            {
                var res = await this.lostService.EditLost(Lost);
                if (res)
                {
                    this.eventAggregator.GetEvent<EditLostEvent>().Publish(Lost);
                    Navigate("ViewLosts");

                    this.Lost.PropertyChanged -= Lost_PropertyChanged;

                    this.Lost = new Lost();
                    this.Lost.Institution = new Institution();
                    this.Lost.Institution.City = new City();
                    this.Lost.Institution.InstitutionType = new InstitutionType();
                    this.Lost.PropertyChanged += Lost_PropertyChanged;
                    this.Lost.DetectionTime = DateTime.Now;

                    editProcess = false;
                    Label = "Registration Form";
                }
                else {
                    this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "Error!", Title = "Error" });
                }
            }
            else
            {
                this.Lost.Institution = Institution.Clone() as Institution;
                this.Lost.Institution.InstitutionType = Institution.InstitutionType.Clone() as InstitutionType;
                this.Lost.Institution.City = Institution.City.Clone() as City;
                this.Lost.InstitutionId = Institution.Id;
                var res = this.lostService.AddLost(Lost).Result;
                if (res != null)
                {
                    this.eventAggregator.GetEvent<NewLostEvent>().Publish(res);
                    Navigate("ViewLosts");

                    this.Lost.PropertyChanged -= Lost_PropertyChanged;
                    this.Lost = new Lost();
                    this.Lost.Institution = new Institution();
                    this.Lost.Institution.City = new City();
                    this.Lost.Institution.InstitutionType = new InstitutionType();
                    this.Lost.PropertyChanged += Lost_PropertyChanged;
                    this.Lost.DetectionTime = DateTime.Now;
                    Label = "Registration Form";
                }
                else {
                    this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "Error!", Title = "Error" });
                }

            }
        }

        private bool CanExecuteSaveCommand()
        {
            return !(String.IsNullOrWhiteSpace(this.Lost?.FirstName) | String.IsNullOrWhiteSpace(this.Lost?.MiddleName) | String.IsNullOrWhiteSpace(this.Lost?.LastName) |
                String.IsNullOrWhiteSpace(this.Lost?.AgeBegin.ToString()) | String.IsNullOrWhiteSpace(this.Lost?.AgeEnd.ToString()) | String.IsNullOrWhiteSpace(this.Lost?.BodyType) |
                String.IsNullOrWhiteSpace(this.Lost?.EyeColor) | String.IsNullOrWhiteSpace(this.Lost?.HairColor) | String.IsNullOrWhiteSpace(this.Lost?.Height.ToString()) |
                String.IsNullOrWhiteSpace(this.Lost?.Comment) | String.IsNullOrWhiteSpace(this.Lost?.Description) | String.IsNullOrWhiteSpace(this.Lost?.DetectionDescription) |
                String.IsNullOrWhiteSpace(this.Lost?.Signs) | String.IsNullOrWhiteSpace(this.Lost?.ImagePath) | String.IsNullOrWhiteSpace(this.Lost?.Gender) | String.IsNullOrWhiteSpace(this.Lost?.Clothes));
        }

        private DelegateCommand cancelCommand;
        public DelegateCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new DelegateCommand(
                    () => {
                        editProcess = false;
                        Navigate("ViewLosts");
                        Label = "Registration Form";

                        this.Lost.PropertyChanged -= Lost_PropertyChanged;
                        this.Lost = new Lost();
                        this.Lost.Institution = new Institution();
                        this.Lost.Institution.City = new City();
                        this.Lost.Institution.InstitutionType = new InstitutionType();
                        this.Lost.PropertyChanged += Lost_PropertyChanged;
                        this.Lost.DetectionTime = DateTime.Now;

                    }
                ));
            }
        }

        private DelegateCommand openCommand;
        public DelegateCommand OpenCommand
        {
            get
            {
                return openCommand ?? (openCommand = new DelegateCommand(
                    () => {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.FileName = String.Empty;
                        dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                        if (dialog.ShowDialog() == true)
                            this.Lost.ImagePath = System.IO.Path.GetFullPath(dialog.FileName);
                    }
                ));
            }
        }

        private void ExecuteFillFieldsCommand()
        {
            this.Lost.FirstName = "Altun";
            this.Lost.MiddleName = "Nazim";
            this.Lost.LastName = "Mursalov";
            this.Lost.Gender = "Male";
            this.Lost.EyeColor = "Brown";
            this.Lost.HairColor = "Brown";
            this.Lost.Clothes = "White sweater, blue jeans, brown shoes";
            this.Lost.BodyType = "Medium";
            this.Lost.AgeBegin = 20;
            this.Lost.AgeEnd = 20;
            this.Lost.Height = 180;
            this.Lost.ImagePath = "/FindMePrism;component/Resources/user-default.png"; //@"C:\Users\Ibra_yf85\FİndMe\FindMeClient\FindMePrism\user-default.png";
            this.Lost.Description = "Scar on the right hand";
            this.Lost.DetectionDescription = "Found unconscious in the street near the hospital";
            this.Lost.DetectionTime = DateTime.Now;
            this.Lost.Signs = "Scar";
            this.Lost.Comment = "He is awake, and his condition is stable";
        }
    }
}
