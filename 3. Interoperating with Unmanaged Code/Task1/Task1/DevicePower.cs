using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using TestDevicePowerManager.Enums;
using DevicePowerManager.Structs;
using DevicePowerManager.Models;

namespace DevicePowerManager
{
	[Guid("92789020-B9B2-4214-9E15-FF23133DD198")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public partial class DevicePower: IDevicePower
	{
		public PowerInformation GetPowerInfo()
		{
			IntPtr spiPtr = IntPtr.Zero;
			try
			{
				spiPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SYSTEM_POWER_INFORMATION)));
				uint res = CallNtPowerInformation(
					InformationLevel.SystemPowerInformation,
					IntPtr.Zero,
					0,
					spiPtr,
					Marshal.SizeOf(typeof(SYSTEM_POWER_INFORMATION))
				);

				var spi = Marshal.PtrToStructure<SYSTEM_POWER_INFORMATION>(spiPtr);

				if (res == 0)
				{
					return new PowerInformation
					{
						CoolingMode = spi.CoolingMode,
						Idleness = spi.Idleness,
						MaxIdlenessAllowed = spi.MaxIdlenessAllowed,
						TimeRemaining = spi.TimeRemaining
					};
				}
				else
				{
					throw new Exception();
				}
			}
			finally
			{
				if (spiPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(spiPtr);
				}
			}
		}

		public long GetLastSleepTime()
		{
			IntPtr outputPtr = IntPtr.Zero;

			int size = Marshal.SizeOf(typeof(ulong));
			try
			{
				outputPtr = Marshal.AllocHGlobal(size);

				uint res = CallNtPowerInformation(
					InformationLevel.LastSleepTime,
					IntPtr.Zero,
					0,
					outputPtr,
					size
				);

				long nano100 = Marshal.ReadInt64(outputPtr);

				long sec = nano100 / 10000000;

				if (res == 0)
				{
					return sec;
				}
				else
				{
					throw new Exception();
				}
			}
			finally
			{
				if (outputPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(outputPtr);
				}
			}
		}

		public long GetLastWakeTime()
		{
			IntPtr outputPtr = IntPtr.Zero;

			int size = Marshal.SizeOf(typeof(ulong));
			try
			{
				outputPtr = Marshal.AllocHGlobal(size);

				uint res = CallNtPowerInformation(
					InformationLevel.LastWakeTime,
					IntPtr.Zero,
					0,
					outputPtr,
					size
				);

				long nano100 = Marshal.ReadInt64(outputPtr);

				long sec = nano100 / 10000000;

				if (res == 0)
				{
					return sec;
				}
				else
				{
					throw new Exception();
				}
			}
			finally
			{
				if (outputPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(outputPtr);
				}
			}
		}

		public BatteryState GetSystemBatteryState()
		{
			IntPtr outputPtr = IntPtr.Zero;

			int size = Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE));
			try
			{
				outputPtr = Marshal.AllocHGlobal(size);

				uint res = CallNtPowerInformation(
					InformationLevel.SystemBatteryState,
					IntPtr.Zero,
					0,
					outputPtr,
					size
				);

				var sbs = Marshal.PtrToStructure<SYSTEM_BATTERY_STATE>(outputPtr);

				if (res == 0)
				{
					return new BatteryState
					{
						AcOnLine = sbs.AcOnLine,
						BatteryPresent = sbs.BatteryPresent,
						Charging = sbs.Charging,
						Discharging = sbs.Discharging,
						MaxCapacity = sbs.MaxCapacity,
						RemainingCapacity = sbs.RemainingCapacity
					};
				}
				else
				{
					throw new Exception();
				}
			}
			finally
			{
				if (outputPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(outputPtr);
				}
			}
		}

		public void SetHibernationFileState(bool state)
		{
			IntPtr inPtr = IntPtr.Zero;

			int inSize = Marshal.SizeOf<int>();

			try
			{
				inPtr = Marshal.AllocHGlobal(inSize);

				Marshal.WriteInt32(inPtr, state ? 1 : 0);

				uint res = CallNtPowerInformation(
					InformationLevel.SystemReserveHiberFile,
					inPtr,
					inSize,
					IntPtr.Zero,
					0
				);

				if (res != 0)
				{
					throw new Exception();
				}
			}
			finally
			{
				if (inPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(inPtr);
				}
			}
		}

		public bool Suspend()
		{
			return SetSuspendState(false, false, false);
		}
	}
}
