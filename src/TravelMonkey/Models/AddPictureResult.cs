using Xamarin.Forms;

namespace TravelMonkey.Models
{
    public class AddPictureResult
    {
        public string Description { get; }
        public string LandmarkDescription { get; }
        public Color AccentColor { get; }
        public bool Succeeded => !string.IsNullOrEmpty(Description) && AccentColor != Color.Default;

        public AddPictureResult() { }

        public AddPictureResult(string description, Color accentColor, string landmarkDescription = "")
        {
            Description = $"I see {description}";
            AccentColor = accentColor;
            LandmarkDescription = string.IsNullOrWhiteSpace(landmarkDescription) ? "" : $"And I think I recognize {landmarkDescription}";
        }
    }
}