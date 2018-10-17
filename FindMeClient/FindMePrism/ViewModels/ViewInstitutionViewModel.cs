using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Microsoft.Maps.MapControl.WPF;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.ComponentModel;
using System.Windows;

namespace FindMePrism.ViewModels
{
    public class ViewInstitutionViewModel : BindableBase
    {
        private Institution institution;
        public Institution Institution { get => institution; set => SetProperty(ref institution, value); }

        private Visibility visibility;
        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }

        private string label;
        public string Label { get => label; set => SetProperty(ref label, value); }

        private ObservableCollection<InstitutionType> institutionTypes;
        public ObservableCollection<InstitutionType> InstitutionTypes { get => institutionTypes; set => SetProperty(ref institutionTypes, value); }

        public IEventAggregator eventAggregator { get; }
        public IRegionManager regionManager { get; }
        public IInstitutionService institutionService { get; }
        public bool editProcess { get; set; }
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand FillFieldsCommand { get; }

        private ObservableCollection<Pushpin> pushpins;
        public ObservableCollection<Pushpin> Pushpins
        {
            get { return pushpins; }
            set { SetProperty(ref pushpins, value); }
        }

        public ViewInstitutionViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IInstitutionService service)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.institutionService = service;
            InstitutionTypes = new ObservableCollection<InstitutionType>();
            Institution = new Institution() {
                City = new City(),
                InstitutionType = new InstitutionType()
            };

            Institution.PropertyChanged += Institution_PropertyChanged;
            Pushpins = new ObservableCollection<Pushpin>();
            OkCommand = new DelegateCommand(ExecuteOkCommandAsync, CanExecuteOkCommand);
            editProcess = false;
            Label = "Registration Form";
            Visibility = Visibility.Visible;
            this.eventAggregator.GetEvent<InstTypesEvent>().Subscribe(GetTypes);
            this.eventAggregator.GetEvent<InstToEdit>().Subscribe(GetInstitution);
            this.eventAggregator.GetEvent<AddressEvent>().Subscribe(GetAddress);
            FillFieldsCommand = new DelegateCommand(ExecuteFillFieldsCommand);
        }

        private void GetTypes(IEnumerable<InstitutionType> ts)
        {       
            InstitutionTypes.Clear();
            if (ts != null) {
                foreach (var item in ts) {
                    InstitutionTypes.Add(item);
                }
            }
        }

        private void Institution_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OkCommand.RaiseCanExecuteChanged();
        }

        private void GetAddress(List<string> address)
        {
            if (address != null) {
                this.Institution.City.Name = address[0];
                this.Institution.Address = address[1];
                this.Institution.Latitude = Convert.ToSingle(address[2]);
                this.Institution.Longitude = Convert.ToSingle(address[3]);
            }
            this.eventAggregator.GetEvent<AddressEvent>().Subscribe(GetAddress);
        }

        private void GetInstitution(Institution inst)
        {
            if (inst != null) {
                Institution.PropertyChanged -= Institution_PropertyChanged;
                this.Institution = inst.Clone() as Institution;
                this.Institution.City = inst.City.Clone() as City;
                this.Institution.InstitutionType = inst.InstitutionType.Clone() as InstitutionType;
                Institution.PropertyChanged += Institution_PropertyChanged;
                editProcess = true;
                this.OkCommand.RaiseCanExecuteChanged();
                Visibility = Visibility.Collapsed;
                Label = "Edit Form";
            }
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }


        private async void ExecuteOkCommandAsync()
        {
            try
            {
                if (editProcess) {
                    var res = await this.institutionService.EditInstitution(Institution);
                    if (res) {
                        this.eventAggregator.GetEvent<EditInstEvent>().Publish(Institution);
                        this.Institution.PropertyChanged -= Institution_PropertyChanged;
                        this.Institution = new Institution();
                        this.Institution.City = new City();
                        this.Institution.PropertyChanged += Institution_PropertyChanged;
                        this.eventAggregator.GetEvent<ClearPinsEvent>().Publish(true);
                        editProcess = false;
                        Visibility = Visibility.Visible;
                        Label = "Registration Form";
                        Navigate("ViewAdmin");
                    }
                    else
                        this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "Error", Title = "Error" });
                }
                else {

                    var res = await this.institutionService.AddInstitution(Institution);
                    if (res != null) {
                        this.eventAggregator.GetEvent<NewInstEvent>().Publish(res);
                        this.Institution.PropertyChanged -= Institution_PropertyChanged;
                        this.Institution = new Institution();
                        this.Institution.City = new City();
                        this.Institution.PropertyChanged += Institution_PropertyChanged;
                        this.eventAggregator.GetEvent<ClearPinsEvent>().Publish(true);
                        editProcess = false;
                        Visibility = Visibility.Visible;
                        Label = "Registration Form";
                        Navigate("ViewAdmin");
                    }
                    else
                        this.eventAggregator.GetEvent<ShowAlertEvent>().Publish(new Notification { Content = "Error", Title = "Error" });
                }              
            }
            catch (Exception) { }
        }

        private bool CanExecuteOkCommand()
        {
            if (editProcess)
            {
                return !(String.IsNullOrWhiteSpace(Institution?.InstitutionType?.Type) | String.IsNullOrWhiteSpace(Institution?.Name) | String.IsNullOrWhiteSpace(Institution?.Phone) |
                    String.IsNullOrWhiteSpace(Institution?.City?.Name) | String.IsNullOrWhiteSpace(Institution?.Address) | String.IsNullOrWhiteSpace(Institution?.OpeningHours) |
                    String.IsNullOrWhiteSpace(Institution?.Website));
            }
            else
            {
                if ((String.IsNullOrWhiteSpace(Institution?.Password) && (String.IsNullOrWhiteSpace(Institution?.ConfirmPassword))) == false)
                {
                    if ((Institution?.Password == Institution?.ConfirmPassword) & (Institution?.Password.Length >=6))
                    {
                        return !(String.IsNullOrWhiteSpace(Institution?.InstitutionType?.Type) | String.IsNullOrWhiteSpace(Institution?.Name) | String.IsNullOrWhiteSpace(Institution?.Phone) |
                         String.IsNullOrWhiteSpace(Institution?.City?.Name) | String.IsNullOrWhiteSpace(Institution?.Address) | String.IsNullOrWhiteSpace(Institution?.OpeningHours) |
                         String.IsNullOrWhiteSpace(Institution?.Website) | String.IsNullOrWhiteSpace(Institution?.Login));
                    }
                    else
                        return false;
                }
                return false;
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
                        Institution.PropertyChanged -= Institution_PropertyChanged;

                        this.Institution = new Institution()
                        {
                            City = new City(),
                            InstitutionType = new InstitutionType()
                        };
                        Institution.PropertyChanged += Institution_PropertyChanged;

                        this.eventAggregator.GetEvent<ClearPinsEvent>().Publish(true);
                        editProcess = false;
                        Visibility = Visibility.Visible;
                        Label = "Registration Form";
                    }
                ));
            }
        }

        private void ExecuteFillFieldsCommand()
        {
            this.Institution.Name = "Hospital №2";
            this.Institution.Phone = "(012) 412-34-56";
            this.Institution.OpeningHours = "07:00 - 23:00";
            this.Institution.Website = "www.hospital2.az";
            this.Institution.Login = "hospital2";
            this.Institution.Password = "hospital2";
            this.Institution.ConfirmPassword = "hospital2";
        }
    }
}
