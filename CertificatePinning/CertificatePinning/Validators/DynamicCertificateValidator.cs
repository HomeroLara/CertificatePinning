using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using CertificatePinning.Helpers;
using CertificatePinning.Models;

namespace CertificatePinning.Validators
{
    public static class DynamicCertificateValidator
    {
        /// <summary>
        /// Should return a boolean value, indicating whether the certificate is considered valid (true) or not (false).
        /// </summary>
        /// <param name="requestMessage">The HTTP request message that triggered the callback.</param>
        /// <param name="certificate">The server's certificate.</param>
        /// <param name="chain">The certificate chain built by the system for the server's certificate.</param>
        /// <param name="sslPolicyErrors">A value that indicates any SSL policy errors that occurred during the certificate validation process.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool ValidateServerPublicKey(HttpRequestMessage requestMessage
            , X509Certificate2 certificate
            , X509Chain chain
            , SslPolicyErrors sslPolicyErrors)
        {
            // Perform default validation first
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                var publicKey = Keys
                    .Hosts?
                    .FirstOrDefault(h => h.HashType == HashType.PublicKey && 
                                         string.Equals(h.HostUrl, requestMessage?.RequestUri?.Host, StringComparison.OrdinalIgnoreCase));

                // get the SHA256 Hash of the public key of the certificate
                var publicKeyHashString = KeyHasher.GetPublicKeyHash(certificate);
                
                if (publicKey != null && !string.IsNullOrWhiteSpace(publicKeyHashString))
                {
                    // Compare the computed hash from the certificate of the request object to the preconfigured hash value
                    if (!string.Equals(publicKeyHashString, publicKey?.Hash.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Server certificate does not match the expected value.");
                    }
                    return true;
                }
            }
            
            // If there are SSL policy errors, consider the certificate invalid
            return false;
        }
        
        /// <summary>
        /// Should return a boolean value, indicating whether the certificate is considered valid (true) or not (false).
        /// </summary>
        /// <param name="requestMessage">The HTTP request message that triggered the callback.</param>
        /// <param name="certificate">The server's certificate.</param>
        /// <param name="chain">The certificate chain built by the system for the server's certificate.</param>
        /// <param name="sslPolicyErrors">A value that indicates any SSL policy errors that occurred during the certificate validation process.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool ValidateServerCertificate(HttpRequestMessage requestMessage
            , X509Certificate2 certificate
            , X509Chain chain
            , SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                var certificateKey = Keys
                    .Hosts?
                    .FirstOrDefault(h => h.HashType == HashType.Certificate && 
                                         string.Equals(h.HostUrl, requestMessage?.RequestUri?.Host, StringComparison.OrdinalIgnoreCase));
                
                // get the SHA256 Hash of the entire certificate
                var certificateHashString = KeyHasher.GetCertificateKeyHash(certificate);

                if (certificateKey != null && !string.IsNullOrWhiteSpace(certificateHashString))
                {
                    // Compare the computed hash from the entire certificate from the request object to the preconfigured hash value
                    if (!string.Equals(certificateHashString, certificateKey?.Hash.ToLowerInvariant(),
                            StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Server certificate does not match the expected value.");
                    }

                    return true;
                }
            }
            
            // If there are SSL policy errors, consider the certificate invalid
            return false;
        }
    }
}