using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FindMePrism.ViewModels
{
    public class ViewLostsViewModel : BindableBase
    {
        private ObservableCollection<Lost> losts;
        public ObservableCollection<Lost> Losts { get => losts; set => SetProperty(ref losts, value); }

        private Institution institution;
        public Institution Institution { get => institution; set => SetProperty(ref institution, value); }

        public IEventAggregator eventAggregator { get; }
        public IRegionManager regionManager { get; }
        private readonly ILostService lostService;

        private Visibility buttonOpenVisibility;
        public Visibility ButtonOpenVisibility { get => buttonOpenVisibility; set => SetProperty(ref buttonOpenVisibility, value);}

        private Visibility buttonCloseVisibilty;
        public Visibility ButtonCloseVisibility { get => buttonCloseVisibilty; set => SetProperty(ref buttonCloseVisibilty, value);}

        public ViewLostsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILostService lostService)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.lostService = lostService;
            this.Losts = new ObservableCollection<Lost>();
            this.Institution = new Institution();
            this.Institution.City = new City();
            this.Institution.InstitutionType = new InstitutionType();
            this.eventAggregator.GetEvent<InstEvent>().Subscribe(GetInstitution);
            this.eventAggregator.GetEvent<LostsEvent>().Subscribe(GetLosts);
            this.eventAggregator.GetEvent<NewLostEvent>().Subscribe(AddNewLost);
            this.eventAggregator.GetEvent<EditLostEvent>().Subscribe(EditLost);
            this.ButtonCloseVisibility = Visibility.Collapsed;
        }

        private void EditLost(Lost l)
        {
            var oldItem = Losts.FirstOrDefault(i => i.Id == l.Id);
            var oldIndex = Losts.IndexOf(oldItem);
            Losts[oldIndex] = l;
        }

        private void AddNewLost(Lost l)
        {
            if (l != null)
                Losts.Add(l);
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void GetLosts(IEnumerable<Lost> ls)
        {
            if (ls != null) {
                foreach (var l in ls) {
                    Losts.Add(l);
                }
            }          
        }

        private void GetInstitution(Institution inst)
        {
            if (inst != null)
                Institution = inst;
        }


        private  DelegateCommand <Lost> deleteLostCommand;
        public  DelegateCommand <Lost> DeleteLostCommand
        {
            get
            {
                return deleteLostCommand ?? (deleteLostCommand = new DelegateCommand<Lost>(
                    async param => {
                      var res =  await this.lostService.RemoveLost(param);
                       if (res) {
                           Losts.Remove(param);
                       }
                    }
                ));
            }
        }

        private DelegateCommand addLostCommand;
        public DelegateCommand AddLostCommand
        {
            get
            {
                return addLostCommand ?? (addLostCommand = new DelegateCommand(
                    () => {
                        Navigate("ViewForm");
                        this.eventAggregator.GetEvent<InstEvent>().Publish(Institution);
                    }
                ));
            }
        }

        private DelegateCommand<Lost> editLostCommand;
        public DelegateCommand<Lost> EditLostCommand
        {
            get
            {
                return editLostCommand ?? (editLostCommand = new DelegateCommand<Lost>(
                    param =>  {
                        Navigate("ViewForm");
                        this.eventAggregator.GetEvent<EditLostEvent>().Publish(param);
                    }
                ));
            }
        }

        private DelegateCommand logoutCommand;
        public DelegateCommand LogoutCommand
        {
            get
            {
                return logoutCommand ?? (logoutCommand = new DelegateCommand(
                    () => {
                        Navigate("ViewLogin");
                        Losts.Clear();
                    }
                ));
            }
        }

        private DelegateCommand openMenuCommand;
        public DelegateCommand OpenMenuCommand
        {
            get
            {
                return openMenuCommand ?? (openMenuCommand = new DelegateCommand(
                    () => {
                        ButtonCloseVisibility = Visibility.Visible;
                        ButtonOpenVisibility = Visibility.Collapsed;
                    }
                ));
            }
        }

        private DelegateCommand closeMenuCommand;
        public DelegateCommand CloseMenuCommand
        {
            get
            {
                return closeMenuCommand ?? (closeMenuCommand = new DelegateCommand(
                    () => {
                        ButtonCloseVisibility = Visibility.Collapsed;
                        ButtonOpenVisibility = Visibility.Visible;
                    }
                ));
            }
        }
    }
}
