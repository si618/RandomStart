using Serilog;
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
                // default to 8 seconds, which is just longer than length of Starting.mp3
                MinimumDelay = 8000; 
            }

            if (!Application.Current.Properties.ContainsKey("StartWindow") ||
                !int.TryParse(Application.Current.Properties["StartWindow"].ToString(), out _startWindow))
            {
                // default to 5 seconds, which is mentioned in Startin.mp3
                StartWindow = 5000; 
            }
            /*
            StartingSound = Application.Current.Properties.ContainsKey("StartingSound")
                ? Application.Current.Properties["StartingSound"].ToString()
                : string.Empty;

            StartedSound = Application.Current.Properties.ContainsKey("StartedSound")
                ? Application.Current.Properties["StartedSound"].ToString()
                : "Start.mp3";
            */
            StartingSound = "Starting.mp3";
            StartedSound = "Started.mp3";

            Log.Information($"Minimum delay: {MinimumDelay} ms");
            Log.Information($"Start window: {StartWindow} ms");
            Log.Information($"Starting sound: {StartingSound}");
            Log.Information($"Started sound: {StartedSound}");
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
                Log.Information($"Minimum delay: {MinimumDelay} ms");
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
                Log.Information($"Start window: {StartWindow} ms");
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
                Log.Information($"Starting sound: {StartingSound}");
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
                Log.Information($"Started sound: {StartedSound}");
            }
        }
    }
}