using System;
using Amoenus.PclTimer;
using AudioManager;
using FreshMvvm;
using PropertyChanged;
using RandomStart.Services;
using Xamarin.Forms;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StartPageModel : FreshBasePageModel
    {
        private readonly IPropertyService _properties;
        private readonly RandomStartService _start;

        public StartPageModel(RandomStartService startService, IPropertyService propertyService)
        {
            _start = startService;
            _properties = propertyService;
            startService.Starting += Starting;
            startService.Started += Started;
        }

        public bool CanStart => !_start.IsRunning;

        public string StartText { get; set; } = "Start"; // TODO: i18n if requested

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
                StartText = "Wait for it..."; // TODO: i18n
            });
        }

        private void Started(object sender, EventArgs e)
        {
            PlaySound(_properties.StartedSound);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartColour = Color.Green;
                StartText = "Go!"; // TODO: i18n
            });

            var timer = new CountDownTimer(TimeSpan.FromMilliseconds(314));
            timer.ReachedZero += (_, __) =>
            {
                StartColour = Color.Red;
                StartText = "Start"; // TODO: i18n
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(nameof(CanStart));
            };
            timer.Start();
        }

        private static async void PlaySound(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                await Audio.Manager.PlaySound(filename);
            }
        }
    }
}