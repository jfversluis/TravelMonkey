using System;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class AddReceiptPage : ContentPage
    {
        private readonly AddReceiptPageViewModel _addReceiptPageViewModel = new AddReceiptPageViewModel();

        public AddReceiptPage()
        {
            InitializeComponent();

            BindingContext = _addReceiptPageViewModel;

            MessagingCenter.Subscribe<AddReceiptPageViewModel>(this, Constants.ReceiptAddedMessage, async (vm) => await Navigation.PopModalAsync(true));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}