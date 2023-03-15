using CertificatePinning.ViewModels;
using Xamarin.Forms;

namespace CertificatePinning.Views
{	
    public partial class TestPage : ContentPage
    {
        private TestViewModel _testViewModel;
        public TestPage()
        {
            InitializeComponent();
            _testViewModel = new TestViewModel();
            this.BindingContext = _testViewModel;
        }
    }
}

