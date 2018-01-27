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
	public partial class SubMateriView : ContentPage
	{
        private materi item;

        public SubMateriView()
        {
            InitializeComponent();
        }

        public SubMateriView(materi item)
        {
            InitializeComponent();
            this.item = item;
            this.BindingContext = new ViewModels.SubMateriViewModel(Navigation,item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as ViewModels.SubMateriViewModel;
            vm.LoadItemsCommand.Execute(null);

        }

        private async void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var subitem = e.SelectedItem as Models.submateri;
            if (subitem == null)
                return;
            await Navigation.PushAsync(new Views.DetailView(item,subitem));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
    }
}