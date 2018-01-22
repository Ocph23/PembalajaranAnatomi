using System;

using Xamarin.Forms;

namespace Mobile
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var serverTap = new TapGestureRecognizer();
            serverTap.Tapped += serverTapAction;
            logo.GestureRecognizers.Add(serverTap);
        }

        private async void serverTapAction(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync( new Views.ServerView());
        }
    }
}
