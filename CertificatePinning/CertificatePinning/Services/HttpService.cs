using System;
using System.Net.Http;
using System.Threading.Tasks;
using CertificatePinning.Validators;

namespace CertificatePinning.Services
{
    public static class HttpService
    {
        public static HttpClient HttpClient;

        public static void InitalizeHttpClient(HttpMessageHandler handler = null)
        {
            if(handler == null)
            {
                var httpHandler = new HttpClientHandler();
                
                // the ServerCertificateCustomValidationCallback is a delegate used with the HttpClient object to
                // provide custom validation for the server's SSL/TLS certificate.
                // This feature is particularly useful when you need to implement custom certificate validation logic,
                // such as certificate pinning or when working with self-signed certificates.
                httpHandler.ServerCertificateCustomValidationCallback = DynamicCertificateValidator.ValidateServerCertificate;
                HttpClient = new HttpClient(httpHandler);
                return;
            }
            HttpClient = new HttpClient(handler);
        }

        public static Task<string> GetContents(string url)
        {
            try
            {
                return HttpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

