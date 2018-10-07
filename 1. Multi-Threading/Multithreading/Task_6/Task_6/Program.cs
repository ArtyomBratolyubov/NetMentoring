using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_6
{
	class Program
	{
		static List<int> list = new List<int>();
		static bool stopFlag;

		static Semaphore sem1 = new Semaphore(1, 1);
		static Semaphore sem2 = new Semaphore(0, 1);

		static void Main(string[] args)
		{

			ThreadPool.QueueUserWorkItem(AddingThread);
			ThreadPool.QueueUserWorkItem(WritingThread);

			Console.ReadKey();
		}

		static void AddingThread(object state)
		{
			for (int i = 0; i < 10; i++)
			{
				sem1.WaitOne();

				list.Add(i);

				Thread.Sleep(1000);
				sem2.Release();
				
			}
			stopFlag = true;
		}

		static void WritingThread(object state)
		{
			while (!stopFlag)
			{
				sem2.WaitOne();

				Console.WriteLine(list[list.Count - 1]);

				sem1.Release();
			}
		}
	}
}
