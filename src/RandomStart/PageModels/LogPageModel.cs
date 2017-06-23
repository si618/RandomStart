using FreshMvvm;
using PropertyChanged;
using RandomStart.Services;
using System;

namespace RandomStart.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LogPageModel : FreshBasePageModel
    {
        public LogPageModel()
        {
            // Default ctor needed for design-time page binding context
        }

        public LogPageModel(LogService logService)
        {
            LogService = logService;
            LogService.Emitted += (obj, args) =>
            {
                Log += Environment.NewLine + args.Message;
            };
        }

        private LogService LogService { get; }

        public string Log { get; set; }
   }
}