using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using TravelMonkey.Data;
using TravelMonkey.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly Timer _slideShowTimer = new Timer(5000);

        public List<Destination> Destinations => MockDataStore.Destinations;
        public ObservableCollection<PictureEntry> Pictures => MockDataStore.Pictures;

        private Destination _currentDestination;
        public Destination CurrentDestination
        {
            get => _currentDestination;
            set => Set(ref _currentDestination, value);
        }

        public Command<string> OpenUrlCommand { get; } = new Command<string>(async (url) =>
        {
            if (!string.IsNullOrWhiteSpace(url))
                await Browser.OpenAsync(url, options: new BrowserLaunchOptions
                {
                    Flags = BrowserLaunchFlags.PresentAsFormSheet,
                    PreferredToolbarColor = Color.SteelBlue,
                    PreferredControlColor = Color.White
                });
        });

        public MainPageViewModel()
        {
            if (Destinations.Count > 0)
            {
                CurrentDestination = Destinations[0];

                _slideShowTimer.Elapsed += (o, a) =>
                {
                    var currentIdx = Destinations.IndexOf(CurrentDestination);

                    if (currentIdx == Destinations.Count - 1)
                        CurrentDestination = Destinations[0];
                    else
                        CurrentDestination = Destinations[currentIdx + 1];
                };
            }
        }

        public void StartSlideShow()
        {
            _slideShowTimer.Start();
            
        }
        public void StopSlideShow()
        {
            _slideShowTimer.Stop();
        }
    }
}