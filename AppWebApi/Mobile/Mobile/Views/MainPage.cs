using System;

using Xamarin.Forms;
using Plugin.MediaManager.Forms;
using Plugin.MediaManager;

namespace Mobile
{
    public class MainPage : TabbedPage
    {
        private NavigationPage materiPage;
        private NavigationPage tentangPage;

        public MainPage()
        {
         
            Title = "Aplikasi Pembelajaran";
            materiPage = new NavigationPage(new Views.MateriView()) { Title = "Materi" };
            tentangPage = new NavigationPage(new AboutPage() ){ Title = "Tentang" };

            this.Children.Add(materiPage);
            this.Children.Add(tentangPage);
        }




        

    }
}
