using Mobile.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    internal class KuisViewModel:BaseViewModel
    {
        private submateri subitem;
        private INavigation navigation;

        public ObservableCollection<soal> Soals { get; set; }
        public Command LoadItemsCommand { get; }

        public KuisViewModel(submateri subitem, INavigation navigation)
        {
            this.subitem = subitem;
            this.navigation = navigation;
            Soals = new ObservableCollection<Models.soal>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            ExecuteLoadItemsCommand(null);
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Soals.Clear();
                var datas = await SoalDataStore.GetItemsAsync(subitem.Id);
                foreach (var item in datas)
                {
                    Soals.Add(item);
                }
            }
            catch (Exception ex)
            {
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