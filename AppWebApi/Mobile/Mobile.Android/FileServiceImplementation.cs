using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Mobile.Droid;
using System.Threading.Tasks;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Implementations;
using Plugin.MediaManager.Abstractions.Enums;
using Android.Media;
using Xamarians.MediaPlayer;
using Plugin.MediaManager.Abstractions;
using Mobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileServiceImplementation))]
namespace Mobile.Droid
{
   
    public class FileServiceImplementation : IFileService
    {
        private MediaPlayer player;

        public FileServiceImplementation()
        {
            player = new MediaPlayer();
        }
        public object AudioService { get; private set; }

        public bool FileExists(string filePath)
        {
            var file = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, filePath);
            return File.Exists(file);
        }

        public string GetFile(string file)
        {
            var res = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, file);
            return res;
          
        }

        public async void PlayMedia(string file)
        {
            var res = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, file);
            await CrossMediaManager.Current.Play(res);
        }


        public async void PlayMediaVideo(string file)
        {
            var res = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, file);
            await CrossMediaManager.Current.Play(res, MediaFileType.Video);
        }


        public void Mute()
        {
            SetVolume(0, 0);
        }
        void SetVolume(double volume, double balance)
        {
            volume = Math.Max(0, volume);
            volume = Math.Min(1, volume);

            balance = Math.Max(0, balance);
            balance = Math.Min(1, balance);

            var right = (balance < 0) ? volume * -1 * balance : volume;
            var left = (balance > 0) ? volume * 1 * balance : volume;

            player.SetVolume((float)left, (float)right);
        }


       
       
    }
}