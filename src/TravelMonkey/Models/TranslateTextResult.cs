using System.Collections.Generic;

namespace TravelMonkey.Models
{
    public class TranslateTextResult
    {
        public string InputLanguage { get; }
        public string InputText { get; set; }
        public Dictionary<string, string> Translations { get; }
        public bool Succeeded => !string.IsNullOrEmpty(InputLanguage) && !string.IsNullOrWhiteSpace(InputText) && Translations?.Count > 0;

        public TranslateTextResult() { }

        public TranslateTextResult(string inputLanguage, string inputText, Dictionary<string, string> translations)
        {
            InputLanguage = inputLanguage;
            InputText = inputText;
            Translations = translations;
        }
    }
}