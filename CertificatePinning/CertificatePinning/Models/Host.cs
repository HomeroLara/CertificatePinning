
namespace CertificatePinning.Models
{

	public class Host
	{
		public string HostUrl { get; set; }
		public string HostName { get; set; }
		public string Hash { get; set; }
		public HashType HashType { get; set; }

		public Host()
		{
		}
	}
}

