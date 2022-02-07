using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MusicRoom.Droid.Views;
using MusicRoom.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(MusicRoom.UI.Pages.YouTubePlayerView), typeof(YouTubeAndroidView))]
namespace MusicRoom.Droid.Views
{
    public class YouTubeAndroidView : FrameLayout, IViewRenderer
    {
        public YouTubeAndroidView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public YouTubeAndroidView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        public void MeasureExactly() => throw new NotImplementedException();

        void OnElementChanged(ElementChangedEventArgs<> e)
        {
            
        }
        private void Initialize()
        {
        }
    }
}
