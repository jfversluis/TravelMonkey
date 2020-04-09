using System.Collections.Generic;

namespace TravelMonkey.Models.CognitiveServices
{
    public class TranslateTextResult
    {
        public string InputLanguage { get; }
        public string InputText { get; set; }
        public Dictionary<string, string> Translations { get; }

        public TranslateTextResult(string inputLanguage, string inputText, Dictionary<string, string> translations)
        {
            InputLanguage = inputLanguage;
            InputText = inputText;
            Translations = translations;
        }
    }
}