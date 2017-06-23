using FreshMvvm;
using RandomStart.PageModels;
using RandomStart.Resources;
using RandomStart.Services;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Themes;

namespace RandomStart
{
    public class App : Application
    {
        public App()
        {
            SetupLogging();
            Register();
            LoadStartPage();
        }

        private void SetupLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LogService()
                .CreateLogger();
        }

        private static void Register()
        {
            // These classes have external dependencies, so are interface based to allow mocking.
            FreshIOC.Container.Register<IAudioService, AudioService>();
            FreshIOC.Container.Register<IPropertyService, PropertyService>();

            // FreshIOC uses default ctor unless told otherwise, which is needed for design-time
            // binding, but at runtime the overloaded ctor is needed to avoid null references.
            FreshIOC.Container.Register<LogPageModel, LogPageModel>()
                .UsingConstructor(() => new LogPageModel(
                    FreshIOC.Container.Resolve<LogService>()));
            FreshIOC.Container.Register<StartPageModel, StartPageModel>()
                .UsingConstructor(() => new StartPageModel(
                    FreshIOC.Container.Resolve<IAudioService>(),
                    FreshIOC.Container.Resolve<IPropertyService>(),
                    FreshIOC.Container.Resolve<RandomStartService>()));
            FreshIOC.Container.Register<PropertyPageModel, PropertyPageModel>()
                .UsingConstructor(() => new PropertyPageModel(
                    FreshIOC.Container.Resolve<IPropertyService>()));
        }

        public void LoadStartPage()
        {
            Resources = new DarkThemeResources();

            var mainPage = new NavigationService();
            mainPage.AddPage<StartPageModel>(AppResources.StartPageText);
            mainPage.AddPage<PropertyPageModel>(AppResources.PropertyPageText);
            mainPage.AddPage<LogPageModel>(AppResources.LogPageText);
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Workaround for https://github.com/jcphlux/XamarinAudioManager/issues/12
            var propertyService = FreshIOC.Container.Resolve<IPropertyService>();
            var audioService = FreshIOC.Container.Resolve<IAudioService>();
            audioService.Play(propertyService.StartingSound, 0.0001f);
            audioService.Play(propertyService.StartedSound, 0.0001f);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}