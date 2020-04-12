using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class AddReceiptPageViewModel : BaseViewModel
    {
        private readonly ReceiptsRecognizerService _receiptsRecognizerService = new ReceiptsRecognizerService();
        private MediaFile _photo;
        private StreamImageSource _photoSource;
        private bool _isPosting;
        private Color _pictureAccentColor = Color.SteelBlue;
        private string _pictureDescription;

        public bool ShowImagePlaceholder => !ShowPhoto;
        public bool ShowPhoto => _photoSource != null;

        public StreamImageSource PhotoSource
        {
            get => _photoSource;
            set
            {
                if (Set(ref _photoSource, value))
                {
                    RaisePropertyChanged(nameof(ShowPhoto));
                    RaisePropertyChanged(nameof(ShowImagePlaceholder));
                }
            }
        }

        public bool IsPosting
        {
            get => _isPosting;
            set => Set(ref _isPosting, value);
        }

        public string ReceiptTotal
        {
            get => _pictureDescription;
            set => Set(ref _pictureDescription, value);
        }

        public Command TakePhotoCommand { get; }
        public Command AddPictureCommand { get; }

        public AddReceiptPageViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());
            AddPictureCommand = new Command(() =>
            {
                if (_photo == null)
                    return;

                MockDataStore.Receipts.Add(new ReceiptEntry());
                MessagingCenter.Send(this, Constants.ReceiptAddedMessage);
            });
        }

        private async Task TakePhoto()
        {
            var result = await UserDialogs.Instance.ActionSheetAsync("What do you want to do?",
                "Cancel", null, null, "Take photo", "Choose photo");

            if (result.Equals("Take photo"))
            {
                _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Full });

                PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            }
            else if (result.Equals("Choose photo"))
            {
                _photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Full });

                PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            }
            else
            {
                return;
            }

            await Post();
        }

        private async Task Post()
        {
            if (_photo == null)
            {
                await UserDialogs.Instance.AlertAsync("Please select an image first", "No image selected");
                return;
            }

            IsPosting = true;

            try
            {
                var pictureStream = _photo.GetStreamWithImageRotatedForExternalStorage();

                var result = await _receiptsRecognizerService.RecognizeReceipt(pictureStream);

                if (!result.Succeeded)
                    ReceiptTotal = "Error while reading receipt";
                else
                    ReceiptTotal = result.Description;
            }
            finally
            {
                IsPosting = false;
            }
        }
    }
}