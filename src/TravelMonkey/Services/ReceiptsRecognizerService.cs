using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelMonkey.Models;
using TravelMonkey.Models.CognitiveServices;

namespace TravelMonkey.Services
{
    public class ReceiptsRecognizerService
    {
        private readonly ReceiptRecognitionResult _errorResult = new ReceiptRecognitionResult { Succeeded = false };

        public async Task<ReceiptRecognitionResult> RecognizeReceipt(Stream imageStream)
        {
            byte[] fileBytes;

            var binaryReader = new BinaryReader(imageStream);
            fileBytes = binaryReader.ReadBytes((int)imageStream.Length);

            var requestData = new ByteArrayContent(fileBytes);//new MultipartFormDataContent();
            //requestData.Add(new ByteArrayContent(fileBytes));
            requestData.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(ApiKeys.FormRecognizerEndpoint);
                request.Content = requestData;
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKeys.FormRecognizerApiKey);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Accepted)
                    return _errorResult;

                var resultUrl = response.Headers.GetValues("Operation-Location").FirstOrDefault();

                if (string.IsNullOrWhiteSpace(resultUrl))
                    return _errorResult;

                using (var resultRequest = BuildRequest(resultUrl))
                {
                    var resultResponse = await SendGetResultRequest(client, resultRequest);
                    var i = 0;

                    while (resultResponse.Status != "succeeded" && i <= 3)
                    {
                        await Task.Delay(1000);
                        resultResponse = await SendGetResultRequest(client, BuildRequest(resultUrl));
                        i++;
                    }

                    if (resultResponse.Status != "succeeded" || resultResponse.AnalyzeResult.DocumentResults[0].Fields.ReceiptType.ValueString != "Itemized")
                        return _errorResult;

                    var receiptItems = resultResponse.AnalyzeResult.DocumentResults.Select(d => d.Fields.Items);
                    double total = 0;

                    foreach (var foo in receiptItems)
                    {
                        foreach (var bar in foo.ValueArray)
                        {
                            total += bar.ValueObject.TotalPrice?.ValueNumber.GetValueOrDefault() ?? 0;
                        }
                        
                    }

                    return new ReceiptRecognitionResult { Succeeded = true, Description = $"Total {total}" };
                }
            }
        }

        private HttpRequestMessage BuildRequest(string url)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url);
            request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKeys.FormRecognizerApiKey);

            return request;
        }

        private async Task<AnalyzeReceiptResult> SendGetResultRequest(HttpClient client, HttpRequestMessage resultRequest)
        {
            HttpResponseMessage resultResponse = await client.SendAsync(resultRequest).ConfigureAwait(false);

            //if (resultResponse.StatusCode != HttpStatusCode.OK)
            //    return _errorResult;

            // Read response as a string.
            string result = await resultResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AnalyzeReceiptResult>(result);
        }
    }
}
