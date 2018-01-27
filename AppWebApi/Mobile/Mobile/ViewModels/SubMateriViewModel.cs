using Mobile.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    internal class SubMateriViewModel:BaseViewModel
    {
        private INavigation navigation;
        private submateri selectedItem;
        private materi item;

        public ObservableCollection<submateri> SubMateris { get; set; }
        public Command LoadItemsCommand { get; private set; }

        public Models.submateri SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }

        public Command VideoCommand { get; }

        public SubMateriViewModel(INavigation navigation, materi item)
        {
            Title = "Sub Materi";
            this.navigation = navigation;
            this.item = item;
            this.navigation = navigation;
            SubMateris = new ObservableCollection<Models.submateri>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
          
           
            
        }

       
        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                SubMateris.Clear();
                var items = await SubMateriDataStore.GetItemsAsync(item.KodeMateri);
                foreach (var item in items)
                {
                    SubMateris.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message =ex.Message,
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