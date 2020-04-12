namespace TravelMonkey
{
	public static class ApiKeys
	{
        #warning You need to set up your API keys.
		public static string ComputerVisionApiKey = "";
		public static string TranslationsApiKey = "";
		public static string FormRecognizerApiKey = "";

		// Change this to the Azure Region you are using
		public static string ComputerVisionEndpoint = "https://westeurope.api.cognitive.microsoft.com/";
		public static string TranslationsEndpoint = "https://api.cognitive.microsofttranslator.com/";
		public static string FormRecognizerEndpoint = "https://westeurope.api.cognitive.microsoft.com/formrecognizer/v2.0-preview/prebuilt/receipt/analyze";
	}
}