using System;

namespace ScannerServiceApi
{
	[Serializable]
	public class ServiceStatus
	{
		/// <summary>
		/// Name supposed to be unique per service(we can add guid later if needed)
		/// </summary>
		public string Name { get; set; }

		public string Status { get; set; }

		public double TimeOut { get; set; }
	}
}
