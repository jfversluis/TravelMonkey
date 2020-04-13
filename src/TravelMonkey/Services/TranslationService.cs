using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelMonkey.Models;
using TravelMonkey.Models.CognitiveServices;

namespace TravelMonkey.Services
{
    public class TranslationService
    {
        public async Task<TranslateTextResult> TranslateText(string inputText)
        {
            var body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(ApiKeys.TranslationsEndpoint + "/translate?api-version=3.0&to=en&to=nl&to=es&to=fr");
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKeys.TranslationsApiKey);

                try
                {
                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    // Read response as a string.
                    var result = await response.Content.ReadAsStringAsync();
                    TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);

                    var bestResult = deserializedOutput.OrderByDescending(t => t.DetectedLanguage.Score).FirstOrDefault();

                    // Iterate over the results and compose the result
                    var translations = new Dictionary<string, string>();

                    foreach (Translation t in bestResult.Translations)
                        translations.Add(t.To, t.Text);

                    return new TranslateTextResult(bestResult.DetectedLanguage.Language, inputText, translations);
                }
                catch
                {
                    return new TranslateTextResult();
                }
            }
        }
    }
}