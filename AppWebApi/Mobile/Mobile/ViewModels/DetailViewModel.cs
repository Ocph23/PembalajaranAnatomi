using System;
using Mobile.Models;
using Xamarin.Forms;
using Mobile.Helpers;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using System.Collections.ObjectModel;

namespace Mobile.ViewModels
{
    internal class DetailViewModel:BaseViewModel
    {
        private INavigation navigation;

        public Command LoadItemsCommand { get; private set; }

        private submateri _submateri;

        public submateri Item
        {
            get { return _submateri; }
            set {
                SetProperty(ref _submateri, value);
            }
        }

        public Command RemoveTagCommand { get; }
        public ObservableCollection<TagItem> Items { get; }

        public DetailViewModel(INavigation navigation,materi item, submateri subitem)
        {
           
            this.navigation = navigation;
            this.Item= subitem;
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommand(x));
            ExecuteLoadItemsCommand(null);

            RemoveTagCommand = new Command((arg) => RemoveTag(arg));
            var tags = new ObservableCollection<TagItem>();
            if(subitem.Topiks!=null)
            {
                foreach (var data in subitem.Topiks)
                {
                    tags.Add(new TagItem { Name = data.JudulTopik, PositionStart = data.PosisiMulai, PositionStop = data.PosisiAkhir });
                }
                Items = tags;
            }
        

        }

        private async void RemoveTag(object arg)
        {
            var tagItem = (TagItem)arg;
            if (tagItem == null)
                return;
            await navigation.PushAsync(new Views.VideoView(Item,tagItem));
        }

        private async void ExecuteLoadItemsCommand(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
               Item= await SubMateriDataStore.GetItemAsync(Item.KodeSubMateri);
                
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