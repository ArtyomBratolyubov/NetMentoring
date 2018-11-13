using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager
{
	public class Config
	{
		public string ReadFolder { get; set; }

		public string DropFolder { get; set; }

		public int TimeOut { get; set; }

		public bool Validate()
		{
			if (string.IsNullOrEmpty(this.ReadFolder))
			{
				return false;
			}

			if (string.IsNullOrEmpty(this.DropFolder))
			{
				return false;
			}

			// Timeout must be greater than 2 seconds.
			if (TimeOut < 2)
			{
				return false;
			}

			return true;
		}
	}
}
