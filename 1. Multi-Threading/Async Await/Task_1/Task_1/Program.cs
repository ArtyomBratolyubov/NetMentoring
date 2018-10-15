using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				CursorHelper.ClearLine(1);
				CursorHelper.Set(0, 1);
				Console.Write("N> ");
				string s = Console.ReadLine();

				if (Int32.TryParse(s, out int n))
				{
					CalculateSumAsync(n);
				}
			}
		}

		static async Task CalculateSumAsync(int n)
		{
			int sum = 0;

			CursorHelper.ClearLine(0);
			CursorHelper.Set(0, 0);
			Console.Write("Calculating...");

			await Task.Factory.StartNew(() =>
			{
				for (int i = 1; i <= n; i++)
				{
					sum += i;
				}

				// Simulate long operation.
				Thread.SpinWait(500000000);
			});
			CursorHelper cursor = new CursorHelper();
			cursor.SaveState();

			CursorHelper.ClearLine(0);
			CursorHelper.Set(0, 0);
			Console.Write("Sum = " + sum);
			cursor.LoadState();
		}
	}
}
