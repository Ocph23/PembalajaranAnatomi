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
	public partial class KuisView : ContentPage
	{
        private submateri subitem;

      
        public KuisView(submateri subitem,string nama)
        {
            InitializeComponent();
            this.subitem = subitem;
            this.BindingContext = new ViewModels.KuisViewModel(subitem,Navigation,nama);
        }

        private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}