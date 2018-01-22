using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Helpers;

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
            if(string.IsNullOrEmpty(Nama.Text))
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Input Nama Anda",
                    Cancel = "OK"
                }, "message");
            }
            else
            await Navigation.PushAsync(new KuisView(subitem,Nama.Text));
        }

        private async void cancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}