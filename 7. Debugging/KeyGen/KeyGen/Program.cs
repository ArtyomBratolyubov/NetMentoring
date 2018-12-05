using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace KeyGen
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] addressBytes = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().GetAddressBytes();

			var date = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
			int[] array = addressBytes.Select((x, n) => x ^ date[n]).Select(x => x * 10).ToArray();

			Console.Write("key=" + string.Join("-", array));

			Console.ReadKey();
		}
	}
}
