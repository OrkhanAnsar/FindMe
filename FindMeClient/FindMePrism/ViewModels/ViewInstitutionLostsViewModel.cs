using FindMePrism.Events;
using FindMePrism.Models;
using FindMePrism.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;


namespace FindMePrism.ViewModels
{
    public class ViewInstitutionLostsViewModel : BindableBase
    {
        private ObservableCollection<Lost> losts;
        public ObservableCollection<Lost> Losts { get => losts; set => SetProperty(ref losts, value); }

        private Institution institution;
        public Institution Institution { get => institution; set => SetProperty(ref institution, value); }

        public IEventAggregator eventAggregator { get; }
        public IRegionManager regionManager { get; }
        public ILostService lostService { get; }

        public ViewInstitutionLostsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILostService lostService)
        {
            Losts = new ObservableCollection<Lost>();
            Institution = new Institution();
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.eventAggregator.GetEvent<InstInfoEvent>().Subscribe(GetInst);
            this.lostService = lostService;
        }

        private async void GetInst(Institution inst)
        {
            if (inst != null) {
                Institution = inst;
                var ls = await this.lostService.GetLosts(inst);
                Losts.Clear();
                if (ls != null) {
                    foreach (var l in ls) {
                        Losts.Add(l);
                    }
                }
            }
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }


        private DelegateCommand backCommand;
        public DelegateCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new DelegateCommand(
                    () => {
                        Navigate("ViewAdmin");
                        Losts.Clear();
                    }
                ));
            }
        }
    }
}
