using System;
using Amoenus.PclTimer;
using AudioManager;
using FreshMvvm;
using PropertyChanged;
using RandomStart.Services;
using Xamarin.Forms;
using RandomStart.Resources;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StartPageModel : FreshBasePageModel
    {
        private readonly IPropertyService _properties;
        private readonly RandomStartService _start;

        public StartPageModel()
        {
            // Default ctor for page binding
        }

        public StartPageModel(RandomStartService startService, IPropertyService propertyService)
        {
            _start = startService;
            _properties = propertyService;
            startService.Starting += Starting;
            startService.Started += Started;
        }

        public bool CanStart => !_start.IsRunning;

        public string StartText { get; set; } = AppResources.StartText;

        public Color StartColour { get; set; } = Color.Red;

        public Command Start => new Command(() => _start.StartRandomTimer());

        private void Starting(object sender, EventArgs e)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            RaisePropertyChanged(nameof(CanStart));       
            PlaySound(_properties.StartingSound);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartColour = Color.Yellow;
                StartText = AppResources.StartingText;
            });
        }

        private void Started(object sender, EventArgs e)
        {
            PlaySound(_properties.StartedSound);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartColour = Color.Green;
                StartText = AppResources.StartedText;
            });

            var timer = new CountDownTimer(TimeSpan.FromMilliseconds(314));
            timer.ReachedZero += (_, __) =>
            {
                StartColour = Color.Red;
                StartText = AppResources.StartText;
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(nameof(CanStart));
            };
            timer.Start();
        }

        private static async void PlaySound(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }
            var effectsOn = Audio.Manager.EffectsOn;
            var effectsVolume = Audio.Manager.EffectsVolume;
            Audio.Manager.EffectsOn = true;
            Audio.Manager.EffectsVolume = 1;    
            await Audio.Manager.PlaySound(filename);
            Audio.Manager.EffectsOn = effectsOn;
            Audio.Manager.EffectsVolume = effectsVolume;
        }
    }
}