using FreshMvvm;
using PropertyChanged;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LogPageModel : FreshBasePageModel
    {
        public LogPageModel()
        {
            // Default ctor for page binding
        }

        public string Log { get; set; }
   }
}