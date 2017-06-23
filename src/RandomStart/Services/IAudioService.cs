namespace RandomStart.Services
{
    /// <summary>Service to play sounds.</summary>
    public interface IAudioService
    {
        /// <summary>Play audio file at specified <paramref name="filename" />.</summary>
        /// <param name="filename">Path to sound file played.</param>
        /// <param name="volume">Optional volume level. Defaults to 1 (loudest).</param>
        void Play(string filename, float volume = 1);
    }
}