using System.Collections.Generic;
using System.Collections.ObjectModel;
using TravelMonkey.Models;

namespace TravelMonkey.Data
{
    public class MockDataStore
    {
		public static ObservableCollection<PictureEntry> Pictures { get; set; }
            = new ObservableCollection<PictureEntry>();

        public static ObservableCollection<ReceiptEntry> Receipts { get; set; }
            = new ObservableCollection<ReceiptEntry>();

        public static List<Destination> Destinations { get; } = new List<Destination>() {
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
	}
}