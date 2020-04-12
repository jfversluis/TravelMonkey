using Xamarin.Forms;
using TravelMonkey.Views;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using TravelMonkey.Data;

[assembly: ExportFont("Lato-Black.ttf", Alias = "LatoBlack")]
[assembly: ExportFont("Lato-Bold.ttf", Alias = "LatoBold")]
[assembly: ExportFont("Lato-Regular.ttf", Alias = "LatoRegular")]
[assembly: ExportFont("fa-regular.otf", Alias = "FontAwesomeRegular")]
[assembly: ExportFont("fa-solid.otf", Alias = "FontAwesomeSolid")]

namespace TravelMonkey
{
    public partial class App : Application
    {
        private string[] searchDestinations = new[] { "Seattle", "Maui", "Amsterdam", "Antarctica" };

        public App()
        {
            var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(ApiKeys.BingImageSearch));


            foreach (var destination in searchDestinations)
            {
                var result = client.Images.SearchAsync(destination, color: "blue", minWidth: 500, minHeight: 500, imageType: "Photo", license: "Public", count: 1, maxHeight: 1200, maxWidth: 1200).Result;

                MockDataStore.Destinations.Add(new Models.Destination
                {
                    Title = destination,
                    ImageUrl = result.Value[0].ContentUrl
                });
            }

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