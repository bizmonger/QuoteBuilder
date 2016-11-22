using Entities;
using Repositories.Core;
using Servers;
using Xamarin.Forms;

namespace QuoteBuilder
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Redundancies in Symbol Declarations", "RECS0001:Class is declared partial but has only one part", Justification = "<Pending>")]
    public partial class App : Application
    {
        Autonomy _autonomy = new Autonomy();

        DataBaseFactory _databaseFactory = new DataBaseFactory();
        ViewFactory _viewFactory = new ViewFactory();
        IOFactory _ioFactory = new IOFactory();
        Profile _profile = null;

        public App()
        {
            InitializeComponent();

            // _databaseFactory.PromiseDBs();
            _ioFactory.PromiseFileReader();

            _profile = new ProfileServer().GetProfile();

            _autonomy.Activate();

            // The root page of your application
            var homePage = new ViewMenu.View(); // SearchCustomers.View();
            MainPage = new NavigationPage(homePage) { BarBackgroundColor = Color.Transparent };
            MainPage.Padding = new Thickness(5);

            _viewFactory.Promise(MainPage);
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}