using Android.App;
using Android.Content.PM;
using Android.OS;
using MusicRoom.UI;
using MusicRoom.UI.Droid2;
using Xamarin.Forms;

namespace MusicRoom.Droid2
{
    [Activity(Label = "RoutingSimpleSample.Droid", Icon = "@drawable/icon", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
