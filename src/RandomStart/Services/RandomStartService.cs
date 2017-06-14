using System;
using System.Diagnostics;
using Amoenus.PclTimer;

namespace RandomStart.Services
{
    /// <summary>Service to trigger random start time.</summary>
    public class RandomStartService
    {
        private readonly IPropertyService _properties;
        private readonly Random _random = new Random(); // seeded from system time

        public RandomStartService(IPropertyService properties)
        {
            _properties = properties;
        }

        /// <summary>
        ///     <c>True</c> when <see cref="StartRandomTimer" /> has been called but has not completed.
        /// </summary>
        public bool IsStarting { get; private set; }

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
            if (IsStarting) return;
            IsStarting = true;

            var minimum = _properties.MinimumDelay;
            var window = minimum + _properties.StartWindow;

            Starting?.Invoke(this, EventArgs.Empty);

            var sleep = _random.Next(minimum, window);
            Debug.WriteLine($"Sleeping for {sleep}ms");

            var timer = new CountDownTimer(TimeSpan.FromMilliseconds(sleep));
            timer.ReachedZero += (_, __) =>
            {
                Debug.WriteLine($"Start!");
                Started?.Invoke(this, EventArgs.Empty);
                IsStarting = false;
            };
            timer.Start();
        }
    }
}