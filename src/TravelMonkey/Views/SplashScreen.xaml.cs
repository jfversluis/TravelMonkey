using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using TravelMonkey.Data;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();

            InitializeData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartPulseAnimation();
        }

        void StartPulseAnimation()
        {
            var animation = new Animation();

            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale, logo.Scale, Easing.Linear, 0, 0.1);
            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale, logo.Scale * 1.05, Easing.Linear, 0.1, 0.4);
            animation.WithConcurrent((f) => logo.Scale = f, logo.Scale * 1.05, logo.Scale, Easing.Linear, 0.4, 1);

            Device.BeginInvokeOnMainThread(() =>
            {
                logo.Animate("Pulse", animation, 16, 1000, repeat: () => true);
            });
        }

        private async void InitializeData()
        {
            var searchDestinations = new[] { "Seattle", "Maui", "Amsterdam", "Antarctica" };

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

            await MainThread.InvokeOnMainThreadAsync(AnimateTransition);

            NavigateToMainPage();
        }

        private async Task AnimateTransition()
        {
            // To at least show the pulse animation. Give a feeling that we're loading the app.
            await Task.Delay(2500);

            // Explode the logo and fade to white, which is what the incoming page comes up as.
            var explodeImageTask = Task.WhenAll(Content.ScaleTo(100, 500, Easing.CubicOut), Content.FadeTo(0, 250, Easing.CubicInOut));
            BackgroundColor = Color.White;
            await explodeImageTask;
        }

        private void NavigateToMainPage()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new MainPage();
            });
        }
    }
}