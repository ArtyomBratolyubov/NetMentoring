using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicePowerManager.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct SYSTEM_BATTERY_STATE
	{
		public byte AcOnLine;
		public byte BatteryPresent;
		public byte Charging;
		public byte Discharging;
		public byte spare1;
		public byte spare2;
		public byte spare3;
		public byte spare4;
		public int MaxCapacity;
		public int RemainingCapacity;
		public int Rate;
		public int EstimatedTime;
		public int DefaultAlert1;
		public int DefaultAlert2;
	}
}
