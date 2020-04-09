using System;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class TranslationResultPage : ContentPage
    {
        private readonly TranslateResultPageViewModel _translateResultPageViewModel =
            new TranslateResultPageViewModel();

        public TranslationResultPage(string inputText)
        {
            InitializeComponent();

            _translateResultPageViewModel.InputText = inputText;

            BindingContext = _translateResultPageViewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}