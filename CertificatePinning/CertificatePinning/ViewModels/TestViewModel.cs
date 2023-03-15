using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using CertificatePinning.Services;

namespace CertificatePinning.ViewModels
{
	public class TestViewModel: BaseViewModel
	{
		private string _defaultCustomUrlText = "https://www.";
		private string _defaultContentText = "..ready to download content";
		private string _defaultStatusText = "Downloading certificate...";
		private string _status;
		public string Status
		{
			get => _status;
			set => SetProperty(ref _status, value);
		}
		
		private string _content;
		public string Content
		{
			get => _content;
			set => SetProperty(ref _content, value);
		}

		private string _customUrl;

		public string CustomUrl
		{
			get => _customUrl;
			set => SetProperty(ref _customUrl, value);
		}
		
		public ICommand GetContentCommand { get; }
		public ICommand ResetContentCommand { get; }
		public ICommand GetContentForCustomUrlCommand { get; }
		public TestViewModel()
		{
			Title = "Test Secure Communications";
			Content = _defaultContentText;
			CustomUrl = _defaultCustomUrlText;
			GetContentCommand = new Command<string>(async (url) => await GetContentCommandHandler(url));
			GetContentForCustomUrlCommand = new Command<string>(async (url) => await GetContentCommandHandler(url));
			ResetContentCommand = new Command(ResetContentCommandHandler);
		}

		private void ResetContentCommandHandler()
		{
			Status = string.Empty;
			CustomUrl = _defaultCustomUrlText;
			Content = _defaultContentText;
		}
		private async Task GetContentCommandHandler(string url)
		{
			if (!string.IsNullOrWhiteSpace(url) && !string.Equals(url, _defaultCustomUrlText, StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					IsBusy = true;
					CustomUrl = url;
					Content = string.Empty;
					await Task.Delay(1000);
					if (!string.IsNullOrWhiteSpace((url)))
					{
						Status = _defaultStatusText;
						Content = await HttpService.GetContents(url);
					}
				}
				catch (Exception ex)
				{
					Content = $"Exception: {ex.Message}";
				}
				finally
				{
					Status = string.Empty;
					IsBusy = false;
				}
			}
		}
	}
}

