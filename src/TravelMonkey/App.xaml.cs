using Xamarin.Forms;
using TravelMonkey.Views;

[assembly: ExportFont("Lato-Black.ttf", Alias = "LatoBlack")]
[assembly: ExportFont("Lato-Bold.ttf", Alias = "LatoBold")]
[assembly: ExportFont("Lato-Regular.ttf", Alias = "LatoRegular")]
[assembly: ExportFont("fa-regular.otf", Alias = "FontAwesomeRegular")]
[assembly: ExportFont("fa-solid.otf", Alias = "FontAwesomeSolid")]

namespace TravelMonkey
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SplashScreen();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}