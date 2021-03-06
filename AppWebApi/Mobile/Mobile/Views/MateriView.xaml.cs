﻿using Mobile.Services;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as ViewModels.MateriViewModel;
            vm.LoadItemsCommand.Execute(null);
        }

        private async void ItemsListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.materi;
            if (item == null)
                return;
            await Navigation.PushAsync(new Views.SubMateriView(item));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        private void keluar_Activated(object sender, EventArgs e)
        {
            DependencyService.Get<IAndroidHelper>().Quit();
        }
    }
}