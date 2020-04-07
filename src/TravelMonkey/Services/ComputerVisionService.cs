using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using TravelMonkey.Models;
using Xamarin.Forms;

namespace TravelMonkey.Services
{
    public class ComputerVisionService
    {
        private readonly ComputerVisionClient _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(ApiKeys.ComputerVisionApiKey))
        {
            Endpoint = ApiKeys.ComputerVisionEndpoint
        };

        public async Task<AddPictureResult> AddPicture(Stream pictureStream)
        {
            var result = await _computerVisionClient.AnalyzeImageInStreamAsync(pictureStream, details: new[] { Details.Landmarks }, visualFeatures: new[] { VisualFeatureTypes.Color, VisualFeatureTypes.Description });

            // Get most likely description
            var description = result.Description.Captions.OrderByDescending(d => d.Confidence).FirstOrDefault()?.Text ?? "nothing! No description found";

            // Get accent color
            var accentColor = Color.FromHex($"#{result.Color.AccentColor}");

            // Determine if there are any landmarks to be seen
            var landmark = result.Categories.FirstOrDefault(c => c.Detail != null && c.Detail.Landmarks.Any());

            var landmarkDescription = "";

            landmarkDescription = landmark != null ? $"I think I see the {landmark.Detail.Landmarks.OrderByDescending(l => l.Confidence).First().Name}"
                    : "";

            // Wrap in our result object and send along
            return new AddPictureResult(description, accentColor, landmarkDescription);
        }
    }
}