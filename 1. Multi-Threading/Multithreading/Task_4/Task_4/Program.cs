using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4
{
	class Program
	{
		static void Main(string[] args)
		{
			Thread t = new Thread(() => { Recursive(10); });
			t.Start();

			Console.ReadKey();
		}

		static void Recursive(int num)
		{
			if (num != 0)
			{
				Console.WriteLine(num);
				num--;

				Thread t = new Thread(() => { Recursive(num); });
				t.Start();
			}
		}
	}
}
