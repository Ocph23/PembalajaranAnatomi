﻿using System;
using Android.Content;
using Android.Runtime;
using Mobile.Droid;
using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Util;

[assembly: ExportRenderer(typeof(TagEntry), typeof(TagEntryRenderer))]
namespace Mobile.Droid
{
    public class TagEntryRenderer : EntryRenderer
    {
        public static void Init()
        {
            new TagEntryRenderer();
        }
    }
}