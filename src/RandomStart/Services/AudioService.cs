using AudioManager;

namespace RandomStart.Services
{
    /// <summary>Service to play sounds using Xamarin audio manager.</summary>
    public class AudioService : IAudioService
    {
        public async void Play(string filename, float volume = 1)
        {
            if (string.IsNullOrEmpty(filename) || volume == 0)
            {
                return;
            }
            var effectsOn = Audio.Manager.EffectsOn;
            var effectsVolume = Audio.Manager.EffectsVolume;
            Audio.Manager.EffectsOn = true;
            Audio.Manager.EffectsVolume = volume;
            await Audio.Manager.PlaySound(filename);
            Audio.Manager.EffectsOn = effectsOn;
            Audio.Manager.EffectsVolume = effectsVolume;
        }
    }
}