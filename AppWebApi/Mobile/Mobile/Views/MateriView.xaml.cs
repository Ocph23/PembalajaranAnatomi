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
    public partial class MateriView : ContentPage
    {
        public MateriView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MateriViewModel(Navigation);
        }

        private async void ItemsListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.materi;
            if (item == null)
                return;
            await Navigation.PushModalAsync(new Views.SubMateriView(item));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
        
    }
}