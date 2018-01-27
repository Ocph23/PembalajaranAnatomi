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
using Mobile.ViewModels;
using Plugin.MediaManager.Abstractions;
using System.Collections.ObjectModel;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoView : ContentPage
    {
        private submateri subitem;
        private MediaPlayerViewModel vm;
        private Downloader foo;
        private TagItem tagItem;

        public VideoView(submateri subitem)
        {
            InitializeComponent();
            this.Load(subitem);
        }

        public VideoView(submateri subitem, TagItem tagItem)
        {
            InitializeComponent();
            this.subitem = subitem;
            this.tagItem = tagItem;
          
            this.Load(subitem);
            //Task.Factory.StartNew(()=> PlaySeek(tagItem));
            

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (vm.MediaPlayer.Status == MediaPlayerStatus.Playing)
                await vm.MediaPlayer.Stop();

            if (DependencyService.Get<IFileService>().FileExists(subitem.Animasi))
            {
                DependencyService.Get<IFileService>().PlayMediaVideo(subitem.Animasi);
            }

            if(tagItem!=null)
                await vm.MediaPlayer.Seek(tagItem.PositionStart);
        }

        protected override void OnDisappearing()
        {
            if (vm.MediaPlayer.Status == MediaPlayerStatus.Playing)
                vm.MediaPlayer.Stop();
            base.OnDisappearing();
        }

      

        private void Load(submateri subitem)
        {
           
            var playTap = new TapGestureRecognizer();
            playTap.Tapped += PlayTap_Tapped;
            play.GestureRecognizers.Add(playTap);

            var pauseTap = new TapGestureRecognizer();
            pauseTap.Tapped += pause_Clicked;
            pause.GestureRecognizers.Add(pauseTap);

            var stopTap = new TapGestureRecognizer();
            stopTap.Tapped += stop_Clicked;
            stop.GestureRecognizers.Add(stopTap);

            var soundTap = new TapGestureRecognizer();
            soundTap.Tapped += sound_Clicked;
            sound.GestureRecognizers.Add(soundTap);

            this.subitem = subitem;
            this.vm = new ViewModels.MediaPlayerViewModel(subitem);
            BindingContext = vm;
            vm.MediaPlayer.VolumeManager.VolumeChanged += VolumeManager_VolumeChanged;

            vm.MediaPlayer.PlayingChanged += MediaPlayer_PlayingChanged;
           
            var muted = DependencyService.Get<IVolumeManager>().Mute;
            if (muted)
            {
                sound.Source = "soundoff";
            }
            else
            {
                sound.Source = "soundOn";
            }

            if (!DependencyService.Get<IFileService>().FileExists(subitem.Animasi))
            {
                Download(subitem);
            }
        }

      

        private async void PlayTap_Tapped(object sender, EventArgs e)
        {
            var a = vm.PlaybackController;
            if (tagItem != null)
            {
                await a.SeekTo(tagItem.PositionStart.TotalSeconds);
            }

           else
                await vm.MediaPlayer.Play();

        }

        private void VolumeManager_VolumeChanged(object sender, VolumeChangedEventArgs e)
        {
          
        }

        private void Download(submateri subitem)
        {
           
            foo = new Downloader();
            foo.InitializeDownload(subitem.Animasi);

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
                            MessagingCenter.Send(new MessagingCenterAlert
                            {
                                Title = "Info",
                                Message = "Download Berhasil...",
                                Cancel = "OK"
                            }, "message");
                            DependencyService.Get<IFileService>().PlayMediaVideo(subitem.Animasi);
                            break;
                        case DownloadFileStatus.FAILED:
                            MessagingCenter.Send(new MessagingCenterAlert
                            {
                                Title = "Error",
                                Message = "Terjadi Kesalahan",
                                Cancel = "OK"
                            }, "message");
                            break;
                        case DownloadFileStatus.CANCELED:
                            MessagingCenter.Send(new MessagingCenterAlert
                            {
                                Title = "Error",
                                Message = "Download File Dibatalkan",
                                Cancel = "OK"
                            }, "message");
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
                        progress.Progress = percentage;
                    }
                }
            };

            foo.StartDownloading(true);
        }

        private void MediaPlayer_PlayingChanged(object sender, PlayingChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                progress.Progress = e.Progress;
                if(tagItem!=null && vm.Position >= tagItem.PositionStop)
                {
                    Task.Factory.StartNew(() => Stoped());
                }
            });

        }

        private async void Stoped()
        {
            var a = vm.PlaybackController;
             await a.Stop();
            await Navigation.PopModalAsync();
        }

        private async void play_Clicked(object sender, EventArgs e)
        {
         
            await vm.MediaPlayer.Play();
          
        }

        private async void pause_Clicked(object sender, EventArgs e)
        {
            var a = vm.PlaybackController;
            await a.PlayPause();
        }

        private async void stop_Clicked(object sender, EventArgs e)
        {

            var a = vm.PlaybackController;
            await a.Stop();


        }

        private void sound_Clicked(object sender, EventArgs e)
        {

            var muted = DependencyService.Get<IVolumeManager>().Mute;
            if (muted)
            {
                DependencyService.Get<IVolumeManager>().Mute = false;
                sound.Source = "soundOn";
            }
            else
            {
                DependencyService.Get<IVolumeManager>().Mute = true;
                sound.Source = "soundoff";
            }
              
        }
    }
}