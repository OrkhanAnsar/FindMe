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


namespace FindMePrism.ViewModels
{
    public class ViewAdminViewModel : BindableBase
    {
        private ObservableCollection<Institution> institutions;
        public ObservableCollection<Institution> Institutions { get => institutions; set => SetProperty(ref institutions, value); }
        public IEventAggregator eventAggregator { get; }
        public IRegionManager regionManager { get; }
        public IInstitutionService institutionService { get; }


        public ViewAdminViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IInstitutionService institutionService)
        {
            Institutions = new ObservableCollection<Institution>();
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.institutionService = institutionService;
            this.eventAggregator.GetEvent<InstsEvent>().Subscribe(GetInstitutions);
            this.eventAggregator.GetEvent<NewInstEvent>().Subscribe(AddInst);
            this.eventAggregator.GetEvent<EditInstEvent>().Subscribe(EditInst);
        }

        private void EditInst(Institution inst)
        {
            var oldItem = Institutions.FirstOrDefault(i => i.Id == inst.Id);
            var oldIndex = Institutions.IndexOf(oldItem);
            Institutions[oldIndex] = inst.Clone() as Institution;
        }

        private void AddInst(Institution inst)
        {
            if (inst != null)
                Institutions.Add(inst);
        }

        private void GetInstitutions(IEnumerable<Institution> insts)
        {
            if (insts != null)
            {
                Institutions.Clear();
                foreach (var i in insts)
                {
                    Institutions.Add(i);
                }
            }
        }

        private void Navigate(string uri)
        {
            if (uri != null)
                this.regionManager.RequestNavigate("ContentRegion", uri);
        }

        private DelegateCommand<Institution> deleteInstitutionCommand;
        public DelegateCommand<Institution> DeleteInstitutionCommand
        {
            get
            {
                return deleteInstitutionCommand ?? (deleteInstitutionCommand = new DelegateCommand<Institution>(
                    async param =>
                    {
                        var res = await this.institutionService.RemoveInstitution(param);
                        if (res)
                            Institutions.Remove(param);
                    }
                ));
            }
        }

        private DelegateCommand addInstitutionCommand;
        public DelegateCommand AddInstitutionCommand
        {
            get
            {
                return addInstitutionCommand ?? (addInstitutionCommand = new DelegateCommand(
                    async () =>
                    {
                        Navigate("ViewInstitution");
                        var types = await this.institutionService.GetInstitutionTypes();
                        this.eventAggregator.GetEvent<InstTypesEvent>().Publish(types);
                    }
                ));
            }
        }


        private DelegateCommand<Institution> editInstitutionCommand;
        public DelegateCommand<Institution> EditInstitutionCommand
        {
            get
            {
                return editInstitutionCommand ?? (editInstitutionCommand = new DelegateCommand<Institution>(
                    async param =>
                    {
                        Navigate("ViewInstitution");
                        var types = await this.institutionService.GetInstitutionTypes();
                        this.eventAggregator.GetEvent<InstTypesEvent>().Publish(types);
                        this.eventAggregator.GetEvent<InstToEdit>().Publish(param);
                    }
                ));
            }
        }

        private DelegateCommand<Institution> infoInstitutionCommand;
        public DelegateCommand<Institution> InfoInstitutionCommand
        {
            get
            {
                return infoInstitutionCommand ?? (infoInstitutionCommand = new DelegateCommand<Institution>(
                    param =>
                    {
                        Navigate("ViewInstitutionLosts");
                        this.eventAggregator.GetEvent<InstInfoEvent>().Publish(param);
                    }
                ));
            }
        }

        private DelegateCommand<Institution> changePasswordCommand;
        public DelegateCommand<Institution> ChangePasswordCommand
        {
            get
            {
                return changePasswordCommand ?? (changePasswordCommand = new DelegateCommand<Institution>(
                    param =>
                    {
                        Navigate("ViewChangePassword");
                        this.eventAggregator.GetEvent<ChangePasswordEvent>().Publish(param);
                    }
                ));
            }
        }  

        private DelegateCommand logoutCommand;
        public DelegateCommand LogoutCommand
        {
            get => logoutCommand ?? (logoutCommand = new DelegateCommand(
                () =>
                {
                    Navigate("ViewLogin");
                }
            ));
        }
    }
}
