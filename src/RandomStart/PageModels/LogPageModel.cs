using FreshMvvm;
using PropertyChanged;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LogPageModel : FreshBasePageModel
    {
        public LogPageModel()
        {
            // Default ctor needed for design-time page binding context
        }

        public string Log { get; set; }
   }
}