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
            ExecuteLoadItemsCommand(null);
           


            /*
            SubMateris.Add(new submateri { Id = 1, KodeSubMateri = "SP001", JudulSubMateri = "Usus Besar" });
            SubMateris.Add(new submateri { Id = 2, KodeSubMateri = "SP002", JudulSubMateri = "Lambung" });
            SubMateris.Add(new submateri { Id = 3, KodeSubMateri = "SP003", JudulSubMateri = "Usus Kecil" });
            SubMateris.Add(new submateri { Id = 4, KodeSubMateri = "SP004", JudulSubMateri = "Teggorokan" });
            */
        }

       
        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                SubMateris.Clear();
                var items = await SubMateriDataStore.GetItemsAsync(item.Id);
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