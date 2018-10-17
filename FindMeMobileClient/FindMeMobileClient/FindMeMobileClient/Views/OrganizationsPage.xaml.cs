using FindMeMobileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindMeMobileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrganizationsPage : ContentPage
	{
		public OrganizationsPage ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e) {
            if ((e.Item as Institution).IsVisible == true) {
                (e.Item as Institution).IsVisible = false;
            } else {
                (e.Item as Institution).IsVisible = true;
            }
        }
    }
}