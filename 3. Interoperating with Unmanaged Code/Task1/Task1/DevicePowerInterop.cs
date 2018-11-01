using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestDevicePowerManager.Enums;

namespace DevicePowerManager
{
	public partial class DevicePower
	{
		[DllImport("powrprof.dll")]
		internal static extern uint CallNtPowerInformation(
		   InformationLevel informationLevel,
		   IntPtr lpInputBuffer,
		   int nInputBufferSize,
		   IntPtr lpOutputBuffer,
		   int nOutputBufferSize
		);

		[DllImport("Powrprof.dll", SetLastError = true)]
		internal static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
	}
}
