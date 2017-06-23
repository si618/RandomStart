using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using RandomStart.Resources;
using Xamarin.Forms;

namespace RandomStart.Services
{
    /// <summary>Application navigation.</summary>
    /// <remarks>Modified from FreshMasterDetailNavigationContainer to allow styling.</remarks>
    public class NavigationService : MasterDetailPage, IFreshNavigationService
    {
        private readonly List<Page> _pagesInner = new List<Page>();

        public NavigationService()
        {
            NavigationServiceName = Constants.DefaultNavigationServiceName;
            Initialise();
        }

        public ContentPage MenuPage { get; set; }
        public Dictionary<string, Page> Pages { get; } = new Dictionary<string, Page>();
        private NavigationPage NavigationDetail => Detail as NavigationPage;

        protected ObservableCollection<string> PageNames { get; } = new ObservableCollection<string>();

        public string NavigationServiceName { get; }

        public Task PushPage(Page page, FreshBasePageModel model, bool modal = false,
            bool animate = true)
        {
            return modal
                ? Navigation.PushModalAsync(CreateContainerPageSafe(page))
                : NavigationDetail.PushAsync(page, animate);
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            return modal
                ? Navigation.PopModalAsync(animate)
                : NavigationDetail.PopAsync(animate);
        }

        public Task PopToRoot(bool animate = true)
        {
            return NavigationDetail.PopToRootAsync(animate);
        }

        public void NotifyChildrenPageWasPopped()
        {
            var master = Master as NavigationPage;
            master?.NotifyAllChildrenPopped();
            foreach (var page in Pages.Values.OfType<NavigationPage>())
            {
                (page).NotifyAllChildrenPopped();
            }
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>()
            where T : FreshBasePageModel
        {
            var tabIndex = _pagesInner
                .FindIndex(o => o.GetModel().GetType().FullName == typeof (T).FullName);

            Detail = Pages.Values.ElementAt(tabIndex);

            return Task.FromResult(NavigationDetail.CurrentPage.GetModel());
        }

        private void Initialise()
        {
            CreateMenuPage(AppResources.MenuPageTitle, "Resources.Menu.png");
            RegisterNavigation();
        }

        protected virtual void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public virtual void AddPage<T>(string title, object data = null)
            where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            _pagesInner.Add(page);
            var navigationContainer = CreateContainerPage(page);
            Pages.Add(title, navigationContainer);
            PageNames.Add(title);
            if (Pages.Count == 1)
            {
                Detail = navigationContainer;
            }
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
            {
                return page;
            }
            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
            return new NavigationPage(page);
        }

        protected virtual void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            MenuPage = new ContentPage {Title = menuPageTitle};

            var listView = new ListView {ItemsSource = PageNames};
            listView.ItemSelected += (sender, args) =>
            {
                if (Pages.ContainsKey((string)args.SelectedItem))
                {
                    Detail = Pages[(string)args.SelectedItem];
                }
                IsPresented = false;
            };

            MenuPage.Content = listView;

            var navPage = new NavigationPage(MenuPage) {Title = AppResources.MenuPageTitle};

            if (!string.IsNullOrEmpty(menuIcon))
            {
                navPage.Icon = menuIcon;
            }

            Master = navPage;
        }
    }
}