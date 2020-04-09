using System.Collections.Generic;
using TravelMonkey.Services;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class TranslateResultPageViewModel : BaseViewModel
    {
        private readonly TranslationService _translationService =
            new TranslationService();

        private string _inputText;
        private Dictionary<string, string> _translations;

        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText == value)
                    return;

                Set(ref _inputText, value);

                TranslateText();
            }
        }

        public Dictionary<string, string> Translations
        {
            get => _translations;
            set
            {
                Set(ref _translations, value);
            }
        }

        public Command<string> TranslateTextCommand => new Command<string>((inputText) =>
        {
            InputText = inputText;
        });

        private async void TranslateText()
        {
            var result = await _translationService.TranslateText(_inputText);

            Translations = result.Translations;
        }
    }
}