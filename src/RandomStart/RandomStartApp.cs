using FreshMvvm;
using RandomStart.PageModels;
using RandomStart.Resources;
using RandomStart.Services;
using Xamarin.Forms;

namespace RandomStart
{
    public class App : Application
    {
        public App()
        {
            Register();
            LoadStartPage();
        }

        private static void Register()
        {
            FreshIOC.Container.Register<IAudioService, AudioService>();
            FreshIOC.Container.Register<IPropertyService, PropertyService>();
            
            // FreshIOC uses default ctor unless told otherwise, which is needed for design-time
            // binding context, but at runtime overloaded ctor is needed to avoid null references.
            FreshIOC.Container.Register<StartPageModel, StartPageModel>()
                .UsingConstructor(() => new StartPageModel(
                    FreshIOC.Container.Resolve<IAudioService>(),
                    FreshIOC.Container.Resolve<IPropertyService>(),
                    FreshIOC.Container.Resolve<RandomStartService>()));
            FreshIOC.Container.Register<PropertyPageModel, PropertyPageModel>()
                .UsingConstructor(() => new PropertyPageModel(
                    FreshIOC.Container.Resolve<IAudioService>(),
                    FreshIOC.Container.Resolve<IPropertyService>()));
        }

        public void LoadStartPage()
        {
            MainPage = FreshPageModelResolver.ResolvePageModel<StartPageModel>();
        }

        public void LoadMenuPage()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init(AppResources.MenuPageTitle, "Resources.Menu.png");
            masterDetailNav.AddPage<PropertyPageModel>(AppResources.PropertyPageText, null);
            masterDetailNav.AddPage<LogPageModel>(AppResources.LogPageText, null);
            MainPage = masterDetailNav;
        }

        public void LoadPropertiesPage()
        {
            //MainPage = FreshPageModelResolver.ResolvePageModel<PropertyPageModel>();
        }

        public void LoadLogPage()
        {
            //MainPage = FreshPageModelResolver.ResolvePageModel<LogPageModel>();
        }

        protected override void OnStart()
        {
            // Workaround for https://github.com/jcphlux/XamarinAudioManager/issues/12
            var propertyService = FreshIOC.Container.Resolve<IPropertyService>();
            var audioService = FreshIOC.Container.Resolve<IAudioService>();
            audioService.Play(propertyService.StartingSound, 0.001f);
            audioService.Play(propertyService.StartedSound, 0.001f);
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