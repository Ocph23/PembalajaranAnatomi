using System;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    internal class MainMenuViewModel
    {
        private INavigation navigation;

        public Command ShowMaterisCommand { get; }
        public Command ShowAboutCommand { get; }
        public Command CloseCommand { get; }

        public MainMenuViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            ShowMaterisCommand = new Command((x) => ShowMateriAction(x));
            ShowAboutCommand = new Command((x) => ShowAboutAction(x));
            CloseCommand = new Command((x) => CloseAction(x));

        }

        private void CloseAction(object x)
        {
          //  Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ShowAboutAction(object x)
        {
            navigation.PushAsync(new AboutPage());
        }

        private void ShowMateriAction(object x)
        {
            navigation.PushAsync(new Views.MateriView());
        }
    }
}