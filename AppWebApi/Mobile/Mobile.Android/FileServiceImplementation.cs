using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobile.Services;
using System.IO;
using Mobile.Droid;
using System.Threading.Tasks;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Implementations;
using Plugin.MediaManager.Abstractions.Enums;

[assembly: Xamarin.Forms.Dependency(typeof(FileServiceImplementation))]
namespace Mobile.Droid
{
   
    public class FileServiceImplementation : IFileService
    {
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
            await CrossMediaManager.Current.Play(res, MediaFileType.Video);
        }

    }
}