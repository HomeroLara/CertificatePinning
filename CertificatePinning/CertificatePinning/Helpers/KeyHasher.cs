using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace CertificatePinning.Helpers
{
	public static class KeyHasher
	{
		/// <summary>
		/// This method takes an X509Certificate2 object as a parameter and returns a
		/// SHA256 hash of the public key of the certificate
		/// </summary>
		/// <param name="certificate"></param>
		/// <returns></returns>
		public static string GetPublicKeyHash(X509Certificate2 certificate)
		{
			try
			{   
				// Retrieve the public key from the X509 certificate and assign it to a variable
				var publicKey = certificate.GetPublicKey();
				
				// Create a new SHA256 hash algorithm, compute the hash of the public key
				var publicKeyHash = SHA256.Create().ComputeHash(publicKey);
				
				// Convert the public key hash to a lowercase hexadecimal string and return it
				return BitConverter.ToString(publicKeyHash).Replace("-", "").ToLowerInvariant();
			}
			catch (Exception ex)
			{
				return null;
			} 
		}
		
		/// <summary>
		/// This method takes an X509Certificate2 object as a parameter and returns a
		/// SHA256 hash of the entire certificate
		/// </summary>
		/// <param name="certificate"></param>
		/// <returns></returns>
		public static string GetCertificateKeyHash(X509Certificate2 certificate)
		{
			try
			{  
				// Create a new SHA256 hash algorithm, compute the hash of the entire certificate   
				var certificateHash = SHA256.Create().ComputeHash(certificate.RawData);
				
				// Convert the public key hash to a lowercase hexadecimal string and return it
				return BitConverter.ToString(certificateHash).Replace("-", "").ToLowerInvariant();
			}
			catch (Exception ex)
			{
				return null;
			} 
		}
	}
}

