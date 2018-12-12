using System;

namespace WebApplication.Services
{
	public class SomeService : IService
	{
		public string StringGuid()
		{
			return Guid.NewGuid().ToString();
		}

		public int Sum(int a, int b)
		{
			return a + b;
		}
	}
}