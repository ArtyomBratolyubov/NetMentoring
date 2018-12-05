using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ScannerManager
{
	public class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(s =>
			{
				s.Service<ScannerManagerService>();
				s.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
				s.SetDisplayName(ConfigurationManager.AppSettings["DisplayName"]);
				s.StartAutomatically();
				s.RunAsLocalService();
			});
		}
	}
}
