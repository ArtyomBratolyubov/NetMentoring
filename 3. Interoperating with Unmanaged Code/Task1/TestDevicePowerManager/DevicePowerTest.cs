using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevicePowerManager;

namespace TestdevicePowerManager
{
	[TestClass]
	public class TestdevicePower
	{
		private IDevicePower devicePower;

		[TestInitialize]
		public void TestInit()
		{
			devicePower = new DevicePower();
		}

		[TestMethod]
		public void TestGetPowerInfo()
		{
			var spi = devicePower.GetPowerInfo();

			Console.WriteLine(spi.CoolingMode);
			Console.WriteLine(spi.Idleness);
			Console.WriteLine(spi.MaxIdlenessAllowed);
			Console.WriteLine(spi.TimeRemaining);
		}

		[TestMethod]
		public void TestGetLastSleepTime()
		{
			long time = devicePower.GetLastSleepTime();

			Console.WriteLine("Last sleep time = " + time);
		}

		[TestMethod]
		public void TestGetLastWakeTime()
		{
			long time = devicePower.GetLastWakeTime();

			Console.WriteLine("Last wake time = " + time);
		}

		[TestMethod]
		public void TestGetSystemBatteryState()
		{
			var sbs = devicePower.GetSystemBatteryState();

			//Console.WriteLine(sbs.AcOnLine == 1 ? "AC adapter online" : "AC adapter offline");
			//Console.WriteLine(sbs.BatteryPresent == 1 ? "Battery installed" : "Battery not installed");
			//Console.WriteLine(sbs.Charging == 1 ? "Charging" : "Discharging");
			//Console.WriteLine("Charge = {0:0}%", (double)sbs.RemainingCapacity / sbs.MaxCapacity * 100);
		}

		[TestMethod]
		public void TestSetHibernationFileStateFalse()
		{
			devicePower.SetHibernationFileState(false);
		}

		[TestMethod]
		public void TestSetHibernationFileStateTrue()
		{
			devicePower.SetHibernationFileState(true);
		}

		[TestMethod]
		public void TestSuspend()
		{
			// Be carefull
			devicePower.Suspend();
		}
	}
}
