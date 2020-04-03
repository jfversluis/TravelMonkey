using Xamarin.Forms;

[assembly: ExportFont("Lato-Black.ttf")]
[assembly: ExportFont("Lato-Bold.ttf")]
[assembly: ExportFont("Lato-Regular.ttf")]

namespace TravelMonkey
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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