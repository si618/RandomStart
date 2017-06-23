using FreshMvvm;
using PropertyChanged;
using RandomStart.Resources;
using RandomStart.Services;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class PropertyPageModel : FreshBasePageModel
    {
        private readonly IPropertyService _propertyService;

        public PropertyPageModel()
        {
            // Default ctor needed for design-time page binding context
        }

        public PropertyPageModel(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public string MinimumDelayLabel => AppResources.MinimumDelayLabel;
        public string StartWindowLabel => AppResources.StartWindowLabel;
        public string StartingSoundLabel => AppResources.StartingSoundLabel;
        public string StartedSoundLabel => AppResources.StartedSoundLabel;

        public int MinimumDelay
        {
            get { return _propertyService.MinimumDelay; }
            set { _propertyService.MinimumDelay = value; }
        }

        public int StartWindow
        {
            get { return _propertyService.StartWindow; }
            set { _propertyService.StartWindow = value; }
        }

        public string StartingSound
        {
            get { return _propertyService.StartingSound; }
            set { _propertyService.StartingSound = value; }
        }

        public string StartedSound
        {
            get { return _propertyService.StartedSound; }
            set { _propertyService.StartedSound = value; }
        }
    }
}