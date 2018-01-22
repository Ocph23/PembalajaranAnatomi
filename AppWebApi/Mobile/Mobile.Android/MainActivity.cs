using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.MediaManager.Forms.Android;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;
using System.Linq;

namespace Mobile.Droid
{
    using AsNum.XFControls.Droid;

    [Activity(Label = "Mobile.Android", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
     //   private NotificationClickedBroadcastReceiver _receiverNotificationClicked;
        protected override void OnResume()
        {
            base.OnResume();
        //    _receiverNotificationClicked = new NotificationClickedBroadcastReceiver();
           // RegisterReceiver(
              //  _receiverNotificationClicked,
             //   new IntentFilter(DownloadManager.ActionNotificationClicked)
           // );
        }

        protected override void OnPause()
        {
            base.OnPause();

     //       UnregisterReceiver(_receiverNotificationClicked);
        }
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.OnCreate(bundle);
            
            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file => {
            #if __IOS__
                        string fileName = (new NSUrl(file.Url, false)).LastPathComponent;
                        return Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), fileName);
            #elif __ANDROID__
                            string fileName = file.Url.Split('=').Last();
                            var res= Path.Combine(ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
                             return res;
            #else
                        string fileName = '';
                        return Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), fileName);
            #endif
            });


            (CrossDownloadManager.Current as DownloadManagerImplementation).IsVisibleInDownloadsUi = true;


            global::Xamarin.Forms.Forms.Init(this, bundle);
           
            VideoViewRenderer.Init();
            TagEntryRenderer.Init();
            AsNumAssemblyHelper.HoldAssembly();
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}