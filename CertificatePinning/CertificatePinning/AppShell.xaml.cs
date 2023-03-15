using System;
using System.Collections.Generic;
using CertificatePinning.ViewModels;
using CertificatePinning.Views;
using Xamarin.Forms;

namespace CertificatePinning
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

