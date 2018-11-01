using DevicePowerManager.Models;
using DevicePowerManager.Structs;
using System.Runtime.InteropServices;

namespace DevicePowerManager
{
	[Guid("B7B59249-F119-4406-996A-36A9DB039327")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IDevicePower
	{
		PowerInformation GetPowerInfo();

		long GetLastSleepTime();

		long GetLastWakeTime();

		BatteryState GetSystemBatteryState();

		void SetHibernationFileState(bool state);

		bool Suspend();
	}
}
