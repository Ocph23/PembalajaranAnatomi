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
using Mobile.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidHelper))]
namespace Mobile.Droid
{
    public class AndroidHelper : IAndroidHelper
    {
        public void Quit()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}