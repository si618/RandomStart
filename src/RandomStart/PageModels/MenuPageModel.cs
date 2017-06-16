using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MenuPageModel : FreshBasePageModel
    {
        public MenuPageModel()
        {
            // Default ctor needed for design-time page binding context
        }

        public Command ShowProperties
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<PropertyPageModel>();
                });
            }
        }

        public Command ShowLog
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<LogPageModel>();
                });
            }
        }
    }
}