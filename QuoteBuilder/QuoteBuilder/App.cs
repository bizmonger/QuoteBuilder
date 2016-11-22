//using Repositories;
//using Repositories.Details;
//using Xamarin.Forms;

//namespace QuoteBuilder
//{
//    public partial class App : Application
//    {
//        public App()
//        {
//            _databaseFactory.PromiseDBs();
//            _ioFactory.PromiseFileReader();

//            _profile = new ProfileServer().GetProfile();

//            _autonomy.Activate();

//            // The root page of your application
//            var homePage = new ViewMenu.View();
//            MainPage = new NavigationPage(homePage) { BarBackgroundColor = Color.Transparent };
//            MainPage.Padding = new Thickness(5);

//            _viewFactory.Promise(MainPage);
//        }

//        protected override void OnStart() { }

//        protected override void OnSleep() { }

//        protected override void OnResume() { }
//    }
//}