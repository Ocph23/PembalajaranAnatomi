using System;
using Mobile.Models;
using Xamarin.Forms;
using Mobile.Helpers;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;

namespace Mobile.ViewModels
{
    internal class DetailViewModel:BaseViewModel
    {
        private INavigation navigation;
        private submateri subitem;
       

        public Command LoadItemsCommand { get; private set; }

        private submateri _submateri;

        public submateri Item
        {
            get { return _submateri; }
            set {
                SetProperty(ref _submateri, value);
            }
        }

        public DetailViewModel(INavigation navigation,materi item, submateri subitem)
        {
           
            this.navigation = navigation;
            this.Item= subitem;
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            ExecuteLoadItemsCommand(null);
          
        }


        private void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
              //  Item = await SubMateriDataStore.GetItemAsync(subitem.Id.ToString());
                
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
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