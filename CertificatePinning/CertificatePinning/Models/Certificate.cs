using System.Security.Cryptography.X509Certificates;

namespace CertificatePinning.Models
{
    public class Certificate
    {
        public X509Certificate2 X509Certificate2 { get; set; }
        public Host Host { get; set; }
        public Certificate()
        {
            
        }
    }
}