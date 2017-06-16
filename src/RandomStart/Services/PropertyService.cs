using Xamarin.Forms;

namespace RandomStart.Services
{
    /// <summary>Service to store and retrieve property settings from application.</summary>
    public class PropertyService : IPropertyService
    {
        private int _minimumDelay;
        private string _startedSound;
        private string _startingSound;
        private int _startWindow;

        public PropertyService()
        {
            if (!Application.Current.Properties.ContainsKey("MinimumDelay") ||
                !int.TryParse(Application.Current.Properties["MinimumDelay"].ToString(), out _minimumDelay))
            {
                MinimumDelay = 1000; // default to 1 second
            }

            if (!Application.Current.Properties.ContainsKey("StartWindow") ||
                !int.TryParse(Application.Current.Properties["StartWindow"].ToString(), out _startWindow))
            {
                StartWindow = 5000; // default to 5 seconds
            }

            /*
            StartingSound = Application.Current.Properties.ContainsKey("StartingSound")
                ? Application.Current.Properties["StartingSound"].ToString()
                : string.Empty;

            StartedSound = Application.Current.Properties.ContainsKey("StartedSound")
                ? Application.Current.Properties["StartedSound"].ToString()
                : "Start.mp3";
            */
            StartingSound = "Arm.mp3";
            StartedSound = "Start.mp3";
        }

        public int MinimumDelay
        {
            get { return _minimumDelay; }
            set
            {
                if (_minimumDelay == value) return;
                // TODO: Validation: Must be > 0
                _minimumDelay = value;
                Application.Current.Properties["MinimumDelay"] = _minimumDelay;
            }
        }

        public int StartWindow
        {
            get { return _startWindow; }
            set
            {
                if (_startWindow == value) return;
                // TODO: Validation: Must be > 0
                _startWindow = value;
                Application.Current.Properties["StartWindow"] = _startWindow;
            }
        }

        public string StartingSound
        {
            get { return _startingSound; }
            set
            {
                if (_startingSound == value) return;
                // TODO: Validation: Path must exist
                _startingSound = value;
                Application.Current.Properties["StartingSound"] = _startingSound;
            }
        }

        public string StartedSound
        {
            get { return _startedSound; }
            set
            {
                if (_startedSound == value) return;
                // TODO: Validation: Path must exist
                _startedSound = value;
                Application.Current.Properties["StartedSound"] = _startedSound;
            }
        }
    }
}