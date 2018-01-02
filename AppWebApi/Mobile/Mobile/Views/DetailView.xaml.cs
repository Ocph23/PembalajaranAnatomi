using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.MediaManager;

namespace Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailView : ContentPage
	{
        private materi item;
        private submateri subitem;


        public DetailView(materi item, submateri subitem)
        {
            InitializeComponent();
            this.item = item;
            this.subitem = subitem;
            BindingContext = new ViewModels.DetailViewModel(Navigation,item, subitem);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CrossMediaManager.Current.Status == Plugin.MediaManager.Abstractions.Enums.MediaPlayerStatus.Playing)
                CrossMediaManager.Current.Stop();
            else

          await  CrossMediaManager.Current.Play("http://192.168.1.7/api/media/1/sound");
        }

        private async void video_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new Views.VideoView(subitem));
        }
    }
}