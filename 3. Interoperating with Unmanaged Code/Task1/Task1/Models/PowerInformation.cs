using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicePowerManager.Models
{
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	[Guid("11BA2746-8C53-431D-9369-F2A80DB880EA")]
	public struct PowerInformation
	{
		public int MaxIdlenessAllowed;
		public int Idleness;
		public int TimeRemaining;
		public byte CoolingMode;
	}
}
