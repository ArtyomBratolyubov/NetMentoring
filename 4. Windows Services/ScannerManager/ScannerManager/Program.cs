using System;
using System.Collections.Generic;
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
				// Maybe there is a better way to do it.
				s.AddCommandLineDefinition("config", c => { ScannerManagerService.SetConfigPath(c); });

				s.Service<ScannerManagerService>();
				s.SetServiceName("ScannerManagerService");
				s.SetDisplayName("Scanner Manager Service");
				s.StartAutomatically();
				s.RunAsLocalService();
			});
		}
	}
}
