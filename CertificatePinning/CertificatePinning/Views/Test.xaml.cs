using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using CertificatePinning.Services;

namespace CertificatePinning.Views
{	
	public partial class Test : ContentPage
	{	
		public Test ()
		{
			InitializeComponent ();
		}

        async void Handle_WebView(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WebPage());
        }

        async void Handle_ImageView(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ImagePage());
        }

        async void Handle_HttpClient(object sender, System.EventArgs e)
        {
            base.OnAppearing();
            try
            {
                var result = await SecuredHTTPService.GetContents("https://www.microsoft.com/");
                await DisplayAlert("Success", result, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error occurred: {ex.Message}", "OK");
            }
        }
    }

    public class CustomWebView : WebView
    {

    }

    public class WebPage : ContentPage
    {
        public WebPage()
        {
            /* easy approach
             * var browser = new CustomCrossWebView();
            browser.Open("https://microsoft.com");
            */

            // With custom renderer
            Content = new CustomWebView() { Source = "https://microsoft.com" };
        }
    }

    public class SafeImage : Image
    {
        public SafeImage()
        {
        }

        public async Task Load(string url)
        {
            var stream = await SecuredHTTPService.GetStream(url);
            Source = ImageSource.FromStream(() => stream);
        }
    }


    public class ImagePage : ContentPage
    {
        private SafeImage _image;

        public ImagePage()
        {
            _image = new SafeImage() { IsVisible = false };

            Content = _image;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _image.Load("https://d6rp199oz9kcs.cloudfront.net/dist/images/pages/index/platform-screenshot@2x-mmjfsTTi.png");
            _image.IsVisible = true;
        }
    }
}

