using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CertificatePinning.Helpers;
using CertificatePinning.Models;

namespace CertificatePinning
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Keys.Hosts = new List<Host>();
            Keys.Hosts.Add(new Host(){ HostName = "Microsoft", HostUrl = "www.microsoft.com", Hash = "237c04979ee3ddf7cd7ab13e19eb3bbc203ec83fac24f2572eee04f189860471", HashType = HashType.PublicKey});
            Keys.Hosts.Add(new Host(){ HostName = "Google", HostUrl = "www.google.com", Hash = "0fa992955e1f706c836e534c3099e64268a3814b30f66b270acc4d835874be61", HashType = HashType.PublicKey});
            Keys.Hosts.Add(new Host(){ HostName = "Microsoft", HostUrl = "www.microsoft.com", Hash = "1577e47519986e5dc9eda372c26a3dd19870f1da841e0c1aaf2d1ec255318e33", HashType = HashType.Certificate});
            Keys.Hosts.Add(new Host(){ HostName = "Google", HostUrl = "www.google.com", Hash = "24ae801ae6171048fb5e289d1d873dc717bd0608fb3811b1c795a0f9137f34a9", HashType = HashType.Certificate});
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

