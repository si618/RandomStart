using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomStart
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Source))
            {
                return null;
            }
            return ImageSource.FromResource(Source);
        }
    }
}
