using Amoenus.PclTimer;
using Serilog;
using System;
using System.Diagnostics;

namespace RandomStart.Services
{
    /// <summary>Service to trigger random start time.</summary>
    public class RandomStartService
    {
        private readonly IPropertyService _propertyService;
        private readonly Random _random = new Random(); // seeded from system time

        public RandomStartService(IPropertyService properties)
        {
            _propertyService = properties;
        }

        /// <summary>
        /// <c>True</c> when <see cref="StartRandomTimer" /> has been called but has not completed.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>Event fired when <see cref="StartRandomTimer" /> called.</summary>
        /// <remarks>Won't fire if previous call has not yet finished.</remarks>
        public event EventHandler Starting;

        /// <summary>
        ///     Event fired when <see cref="StartRandomTimer" /> triggered after elapsed random time.
        /// </summary>
        /// <remarks>Won't fire if previous call has not yet finished.</remarks>
        public event EventHandler Started;

        public void StartRandomTimer()
        {
            if (IsRunning)
            {
                Log.Debug($"Timer already running");
                return;
            }
            Log.Debug($"Random start triggered at {DateTime.Now}");
            IsRunning = true;

            var minimum = _propertyService.MinimumDelay;
            var window = minimum + _propertyService.StartWindow;

            Starting?.Invoke(this, EventArgs.Empty);

            var sleep = _random.Next(minimum, window);

            Log.Information($"Sleeping for {sleep}ms");

            var timer = new CountDownTimer(TimeSpan.FromMilliseconds(sleep));
            timer.ReachedZero += (_, __) =>
            {
                Log.Information($"Start!");
                Started?.Invoke(this, EventArgs.Empty);
                IsRunning = false;
            };
            timer.Start();
        }
    }
}