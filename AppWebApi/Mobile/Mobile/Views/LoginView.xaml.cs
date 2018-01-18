using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        private submateri subitem;

        public LoginView(submateri subitem)
        {
            InitializeComponent();
            this.subitem = subitem;
        }

        private async void okButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new KuisView(subitem));
        }

        private async void cancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}