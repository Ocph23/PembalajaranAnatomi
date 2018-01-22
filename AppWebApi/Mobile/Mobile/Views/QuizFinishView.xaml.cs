using Mobile.Services;
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
	public partial class QuizFinishView : ContentPage
	{
        public QuizFinishView(int jumlah, int benar, string nama)
        {
            InitializeComponent();
            UserName.Text = nama;
            benarScore.Text = benar.ToString();
            salahScore.Text = (jumlah - benar).ToString();
            double result = ((Convert.ToDouble(benar) /Convert.ToDouble( jumlah)) * 100);
            score.Text = result.ToString();
        }

        private async void nextLearn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private void close_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAndroidHelper>().Quit();
        }
    }
}