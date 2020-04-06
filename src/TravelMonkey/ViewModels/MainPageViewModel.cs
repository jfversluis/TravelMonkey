using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelMonkey.Models;

namespace TravelMonkey.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public List<Destination> Destinations { get; set; } = new List<Destination>() {
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

        public string[] Places { get; set; } = new string[] { "seattle.jpg", "bali.jpg", "elba.jpg" };
        public string CurrentPlace { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}