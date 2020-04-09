using Xamarin.Forms;
using TravelMonkey.Views;

[assembly: ExportFont("Lato-Black.ttf")]
[assembly: ExportFont("Lato-Bold.ttf")]
[assembly: ExportFont("Lato-Regular.ttf")]

//TODO: Check out why alias doesn't work
[assembly: ExportFont("fa-regular.otf", Alias = "FontAwesomeRegular")]
[assembly: ExportFont("fa-solid.otf", Alias = "FontAwesomeSolid")]

namespace TravelMonkey
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "AppTheme_Experimental" });

            MainPage = new MainPage();
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