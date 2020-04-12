using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using TravelMonkey.Data;
using TravelMonkey.Models;

namespace TravelMonkey.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private Timer _slideShowTimer = new Timer(5000);

        public List<Destination> Destinations => MockDataStore.Destinations;
        public ObservableCollection<PictureEntry> Pictures => MockDataStore.Pictures;

        private Destination _currentDestination;
        public Destination CurrentDestination
        {
            get => _currentDestination;
            set => Set(ref _currentDestination, value);
        }

        public MainPageViewModel()
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