using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicePowerManager.Models
{
	[ComVisible(true)]
	[Guid("7DDD7317-688F-4EB1-BF49-2530B5ADAFE2")]
	[StructLayout(LayoutKind.Sequential)]
	public struct BatteryState
	{
		public int MaxCapacity;
		public int RemainingCapacity;
		public byte AcOnLine;
		public byte BatteryPresent;
		public byte Charging;
		public byte Discharging;
	}
}
