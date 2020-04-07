using System.Collections.Generic;
using System.Timers;
using TravelMonkey.Models;

namespace TravelMonkey.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private Timer _slideShowTimer = new Timer(5000);

        public List<Destination> Destinations { get; set; } = new List<Destination>() {
            new Destination
                 {
                     Title = "Seattle",
                     ImageUrl = "seattle.jpg",
                     Rating = 4.4f,
                     Votes = 3829
                 },
                 new Destination
                 {
                     Title = "Ulun Danu Beratan Temple",
                     ImageUrl = "bali.jpg",
                     Rating = 4.4f,
                     Votes = 3829
                 },
                 new Destination
                 {
                     Title = "Isola d'Elba",
                     ImageUrl = "elba.jpg",
                     Rating = 4.9f,
                     Votes = 9783
                 }
        };

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