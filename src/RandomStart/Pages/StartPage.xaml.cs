using AudioManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomStart.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            Init();
        }

        private static async void Init()
        {
            Audio.Manager.EffectsOn = true;
            var vol = Audio.Manager.EffectsVolume;
            Audio.Manager.EffectsVolume = 0;
            await Audio.Manager.PlaySound("Start.mp3");
            Audio.Manager.EffectsVolume = vol;
        }
    }
}