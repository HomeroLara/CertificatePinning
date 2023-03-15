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
            Keys.Hosts.Add(new Host(){ HostName = "Google", HostUrl = "www.google.com", Hash = "294bd0779551741f40bf1af07b6683da85b79a99d78a3e17cb82804f0f90c20f", HashType = HashType.PublicKey});
            Keys.Hosts.Add(new Host(){ HostName = "Microsoft", HostUrl = "www.microsoft.com", Hash = "1577e47519986e5dc9eda372c26a3dd19870f1da841e0c1aaf2d1ec255318e33", HashType = HashType.Certificate});
            Keys.Hosts.Add(new Host(){ HostName = "Google", HostUrl = "www.google.com", Hash = "115d39ff6cda79ddb209e82f74dfd3e31bdd3154e6307da53c335f7a3681c42e", HashType = HashType.Certificate});
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

