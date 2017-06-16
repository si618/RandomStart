using Amoenus.PclTimer;
using FreshMvvm;
using PropertyChanged;
using RandomStart.Services;
using RandomStart.Resources;
using System;
using Xamarin.Forms;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StartPageModel : FreshBasePageModel
    {
        private readonly IAudioService _audioService;
        private readonly IPropertyService _propertyService;
        private readonly RandomStartService _randomStartService;

        public StartPageModel()
        {
            // Default ctor needed for design-time page binding context
        }

        public StartPageModel(IAudioService audioService, IPropertyService propertyService,
            RandomStartService startService)
        {
            _audioService = audioService;
            _propertyService = propertyService;
            _randomStartService = startService;
            _randomStartService.Starting += Starting;
            _randomStartService.Started += Started;
        }

        public bool CanStart => !_randomStartService.IsRunning;

        public string StartText { get; set; } = AppResources.StartText;

        public Color StartColour { get; set; } = Color.Red;

        public Command Start => new Command(() => _randomStartService.StartRandomTimer());

        private void Starting(object sender, EventArgs e)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            RaisePropertyChanged(nameof(CanStart));       
            _audioService.Play(_propertyService.StartingSound);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartColour = Color.Yellow;
                StartText = AppResources.StartingText;
            });
        }

        private void Started(object sender, EventArgs e)
        {
            _audioService.Play(_propertyService.StartedSound);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartColour = Color.Green;
                StartText = AppResources.StartedText;
            });

            var timer = new CountDownTimer(TimeSpan.FromMilliseconds(250));
            timer.ReachedZero += (_, __) =>
            {
                StartColour = Color.Red;
                StartText = AppResources.StartText;
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(nameof(CanStart));
            };
            timer.Start();
        }
    }
}