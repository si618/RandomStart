using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AudioManager;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace RandomStart.Droid
{
    [Activity(Label = "Random Start", Icon = "@drawable/icon", Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());

            // Prevent app from sleeping
            Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            // Needed for audio manager
            Initializer.Initialize();

            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }
    }
}