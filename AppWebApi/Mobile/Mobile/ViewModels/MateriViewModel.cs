using System.Collections.ObjectModel;
using Mobile.Models;
using Xamarin.Forms;
using System;
using System.Diagnostics;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    internal class MateriViewModel:BaseViewModel
    {
        private INavigation navigation;
        private materi selectedItem;

        public ObservableCollection<materi> Materis { get; set; }
        public Command LoadItemsCommand { get; private set; }

        public Models.materi SelectedItem {
            get
            {
                return selectedItem;
            }
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }
        public MateriViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Materis = new ObservableCollection<Models.materi>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
           // ExecuteLoadItemsCommand(null);
          
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Materis.Clear();
                var pegawais = await MateriDataStore.GetItemsAsync(true);
                foreach (var item in pegawais)
                {
                    Materis.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}