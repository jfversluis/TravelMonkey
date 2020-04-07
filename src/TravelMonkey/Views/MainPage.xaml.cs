using System;
using System.ComponentModel;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _mainPageViewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();

            BindingContext = _mainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _mainPageViewModel.StartSlideShow();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _mainPageViewModel.StopSlideShow();
        }

        private void AddNewPicture_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddPicturePage());
        }

        private void AddNewReceipt_Tapped(object sender, EventArgs e) { }
    }
}