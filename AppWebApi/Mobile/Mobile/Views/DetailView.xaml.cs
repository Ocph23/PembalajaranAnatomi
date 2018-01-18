using System;
using Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.MediaManager;
using Mobile.Services;
using Plugin.MediaManager.Abstractions;
using Plugin.DownloadManager.Abstractions;
using Xamarin.Forms.Internals;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailView : ContentPage
	{
        private materi item;
        private submateri subitem;
        private Downloader foo;


        public DetailView(materi item, submateri subitem)
        {
            InitializeComponent();
            this.item = item;
            this.subitem = subitem;
            BindingContext = new ViewModels.DetailViewModel(Navigation,item, subitem);
            if(!string.IsNullOrEmpty(subitem.Gambar))
            {
                if (DependencyService.Get<IFileService>().FileExists(subitem.Gambar))
                {
                    gambar.Source = DependencyService.Get<IFileService>().GetFile(subitem.Gambar);
                }
                else
                {
                    Download(subitem.Gambar);
                }
            }
            else
            {
                //add default image
            }
        }
        private void Download(string fileName)
        {
            foo = new Downloader();
            foo.InitializeDownload(fileName);
            foo.File.PropertyChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("[Property changed] " + e.PropertyName + " -> " + sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null).ToString());

                // Update UI text-fields
                var downloadFile = ((IDownloadFile)sender);
                switch (e.PropertyName)
                {
                    case nameof(IDownloadFile.Status):
                        break;
                    case nameof(IDownloadFile.StatusDetails):
                        break;
                    case nameof(IDownloadFile.TotalBytesExpected):
                        break;
                    case nameof(IDownloadFile.TotalBytesWritten):
                        break;
                }

                // Update UI if download-status changed.
                if (e.PropertyName == "Status")
                {
                    switch (((IDownloadFile)sender).Status)
                    {
                        case DownloadFileStatus.COMPLETED:
                           gambar.Source = DependencyService.Get<IFileService>().GetFile(fileName);
                            break;
                        case DownloadFileStatus.FAILED:
                        case DownloadFileStatus.CANCELED:

                            // Get the path this file was saved to. When you didn't set a custom path, this will be some temporary directory.
                            // var nativeDownloadManager = (Plugin.DownloadManager)ApplicationContext.GetSystemService(DownloadService);
                            // System.Diagnostics.Debug.WriteLine(nativeDownloadManager.GetUriForDownloadedFile(((DownloadFileImplementation)sender).Id));
                            break;
                    }
                }

                // Update UI while donwloading.
                if (e.PropertyName == "TotalBytesWritten" || e.PropertyName == "TotalBytesExpected")
                {
                    var bytesExpected = ((IDownloadFile)sender).TotalBytesExpected;
                    var bytesWritten = ((IDownloadFile)sender).TotalBytesWritten;

                    if (bytesExpected > 0)
                    {
                        var percentage = Math.Round(bytesWritten / bytesExpected * 100);
                    }
                }
            };

            foo.StartDownloading(true);
        }
      
        private async void video_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new Views.VideoView(subitem));
        }

        private async void kuis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.LoginView(subitem));
        }
    }
}