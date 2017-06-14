namespace RandomStart.Services
{
    /// <summary>Service to store and retrieve property settings.</summary>
    public interface IPropertyService
    {
        /// <summary>Minimum delay in milliseconds before random timer starts.</summary>
        int MinimumDelay { get; set; }

        /// <summary>Maximum elapsed time in milliseconds before random start triggered.</summary>
        int StartWindow { get; set; }

        /// <summary>Path to sound file played when start button first pushed.</summary>
        string StartingSound { get; set; }

        /// <summary>Path to sound file played when random start triggered.</summary>
        string StartedSound { get; set; }
    }
}