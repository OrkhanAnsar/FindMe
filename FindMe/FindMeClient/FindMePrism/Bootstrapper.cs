using FindMePrism.Services;
using FindMePrism.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FindMePrism
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
             Application.Current.MainWindow.Show();
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<ViewLogin>("ViewLogin");
            Container.RegisterTypeForNavigation<ViewLosts>("ViewLosts");
            Container.RegisterTypeForNavigation<ViewForm>("ViewForm");
            Container.RegisterTypeForNavigation<ViewAdmin>("ViewAdmin");
            Container.RegisterTypeForNavigation<ViewInstitution>("ViewInstitution");
            Container.RegisterTypeForNavigation<ViewInstitutionLosts>("ViewInstitutionLosts");
            Container.RegisterTypeForNavigation<ViewChangePassword>("ViewChangePassword");
            Container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILostService, LostService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IInstitutionService, InstitutionService>(new ContainerControlledLifetimeManager());
        }
    }   
}
