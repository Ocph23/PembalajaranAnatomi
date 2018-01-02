using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Helpers.MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });
           


            MainPage = new MainPage();
            MainPage.BackgroundImage = "HumanAnatomy.jpg";
        }
    }
}