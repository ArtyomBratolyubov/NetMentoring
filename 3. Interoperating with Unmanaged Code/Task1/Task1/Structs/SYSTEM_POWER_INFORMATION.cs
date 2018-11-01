using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicePowerManager.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SYSTEM_POWER_INFORMATION
	{
		public int MaxIdlenessAllowed;
		public int Idleness;
		public int TimeRemaining;
		public byte CoolingMode;
	}
}
