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
using Plugin.MediaManager.Abstractions;
using Android.Media;
using Mobile.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(VolumeImplement))]
namespace Mobile.Droid
{
    public class VolumeImplement : IVolumeManager
    {
        private AudioManager manager;

        public VolumeImplement()
        {
            manager = (AudioManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.AudioService);
        }
        public float CurrentVolume { get => manager.GetStreamVolume(Stream.Music); set => manager.SetStreamVolume(Stream.Music, 1, VolumeNotificationFlags.PlaySound); }
        public float MaxVolume { get => manager.GetStreamMaxVolume(Stream.Music); set => throw new NotImplementedException(); }
        public bool Mute { get => manager.IsStreamMute(Stream.Music); set => manager.SetStreamMute(Stream.Music, value); }

        public event VolumeChangedEventHandler VolumeChanged;



    }
}