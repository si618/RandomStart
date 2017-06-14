using FreshMvvm;
using RandomStart.PageModels;
using RandomStart.Services;
using Xamarin.Forms;

namespace RandomStart
{
    public class App : Application
    {
        public App()
        {
            FreshIOC.Container.Register<IPropertyService, PropertyService>();

            LoadStartPage();
        }

        public void LoadStartPage()
        {
            MainPage = FreshPageModelResolver.ResolvePageModel<StartPageModel>();
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
            // Handle when your app starts
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