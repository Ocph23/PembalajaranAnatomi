using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServerView : ContentPage
	{
		public ServerView ()
		{
			InitializeComponent ();
            server.Text = Helpers.Main.Server;
		}

        private async void simpan_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(server.Text))
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Input Server URL",
                    Cancel = "OK"
                }, "message");
            }
            else
            {
                Helpers.Main.Server = server.Text;
                await Navigation.PopModalAsync();
            }
        }

        private async void batal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}