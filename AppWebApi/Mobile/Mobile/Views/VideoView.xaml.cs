using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.MediaManager;
using Mobile.Helpers;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using Xamarin.Forms.Internals;
using Mobile.Services;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoView : ContentPage
    {
        private submateri subitem;
        private Downloader foo;

        public VideoView(submateri subitem)
        {
            InitializeComponent();
            this.subitem = subitem;
            var filePath = subitem.Animasi;
            if (DependencyService.Get<IFileService>().FileExists(filePath))
            {
                PlayAsync(filePath);
            }
            else
            {
                foo = new Downloader();
                foo.File = CrossDownloadManager.Current.CreateDownloadFile(
                  Helpers.Main.Server + "api/submateri?fileName=" + subitem.Animasi);

                download.Clicked += delegate {
                    // If already downloading, abort it.
                    if (foo.IsDownloading())
                    {
                        foo.AbortDownloading();
                        download.Text = "Download aborted.";
                        return;
                    }

                    download.Text = "Start downloading ...";

                    foo.InitializeDownload();

                    foo.File.PropertyChanged += (sender, e) => {
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
                                    player.Source = "/storage/emulated/0/Android/data/com.Ocph23.Anatomi/files/Download/BodyPart_7411803f-f921-444f-8964-aec79e907d93.mp4";
                                    break;
                                case DownloadFileStatus.FAILED:
                                case DownloadFileStatus.CANCELED:
                                    download.Text = "Downloading finished.";
                                    player.Source = "/storage/emulated/0/Android/data/com.Ocph23.Anatomi/files/Download/BodyPart_7411803f-f921-444f-8964-aec79e907d93.mp4";
                                    // Get the path this file was saved to. When you didn't set a custom path, this will be some temporary directory.

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
                                download.Text = "Downloading (" + percentage + "%)";
                            }
                        }
                    };

                    foo.StartDownloading(true);
                };
            }

        }

        private  void PlayAsync(string filePath)
        {
            DependencyService.Get<IFileService>().PlayMedia(filePath);

        }

        private void OnPauseClicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Pause();
            pause.IsEnabled = false;
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Stop();
            pause.IsEnabled = false;
            stop.IsEnabled = false;
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.VideoPlayer.Play();
            pause.IsEnabled = true;
            stop.IsEnabled = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.StatusChanged += CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged += OnPlayingChanged;
          //  player.Source = "http://192.168.1.8/api/submateri?fileName=BodyPart_7411803f-f921-444f-8964-aec79e907d93.mp4";
            play.Clicked += OnPlayClicked;
            stop.Clicked += OnStopClicked;
            pause.Clicked += OnPauseClicked;
            player.AspectMode = VideoAspectMode.AspectFit;
        }

        private void OnPlayingChanged(object sender, PlayingChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                progress.Progress = e.Progress;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossMediaManager.Current.Stop();
            play.Clicked -= OnPlayClicked;
            stop.Clicked -= OnStopClicked;
            pause.Clicked -= OnPauseClicked;
            CrossMediaManager.Current.StatusChanged -= CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged -= OnPlayingChanged;
        }

        private void CurrentOnStatusChanged(object sender, StatusChangedEventArgs statusChangedEventArgs)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var status = statusChangedEventArgs.Status;
                switch (status)
                {
                    case MediaPlayerStatus.Stopped:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        StatusLabel.Text = "Stopped";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Paused:
                        pause.IsEnabled = true;
                        stop.IsEnabled = true;
                        StatusLabel.Text = "Paused";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Playing:
                        pause.IsEnabled = true;
                        stop.IsEnabled = true;
                        await StatusLabel.FadeTo(0);
                        break;
                    case MediaPlayerStatus.Loading:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        StatusLabel.Text = "Loading";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Buffering:
                        pause.IsEnabled = false;
                        stop.IsEnabled = true;
                        StatusLabel.Text = "Buffering";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Failed:
                        pause.IsEnabled = false;
                        stop.IsEnabled = false;
                        StatusLabel.Text = "Failed";
                        await StatusLabel.FadeTo(1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

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

        }
    }
}