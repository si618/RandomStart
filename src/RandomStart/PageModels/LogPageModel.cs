using FreshMvvm;
using PropertyChanged;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LogPageModel : FreshBasePageModel
    {
        public LogPageModel()
        {
        }

        public string Log { get; set; }
   }
}