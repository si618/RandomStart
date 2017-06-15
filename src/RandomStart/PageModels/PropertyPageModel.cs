using FreshMvvm;
using PropertyChanged;
using RandomStart.Resources;
using RandomStart.Services;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class PropertyPageModel : FreshBasePageModel
    {
        private readonly IPropertyService _properties;

        public PropertyPageModel()
        {
            // Default ctor for page binding
        }
        public PropertyPageModel(IPropertyService propertyService)
        {
            _properties = propertyService;
        }

        public string MinimumDelayLabel => AppResources.MinimumDelayLabel;
        public string StartWindowLabel => AppResources.StartWindowLabel;
        public string StartingSoundLabel => AppResources.StartingSoundLabel;
        public string StartedSoundLabel => AppResources.StartedSoundLabel;

        public int MinimumDelay
        {
            get { return _properties.MinimumDelay; }
            set
            {
                _properties.MinimumDelay = value;
            }
        }

        public int StartWindow
        {
            get { return _properties.StartWindow; }
            set
            {
                _properties.StartWindow = value;
            }
        }

        public string StartingSound
        {
            get { return _properties.StartingSound; }
            set
            {
                _properties.StartingSound = value;
            }
        }

        public string StartedSound
        {
            get { return _properties.StartedSound; }
            set
            {
                _properties.StartedSound = value;
            }
        }
    }
}