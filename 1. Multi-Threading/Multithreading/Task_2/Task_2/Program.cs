using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
	class Program
	{
		static int[] array;
		static Random random = new Random(Environment.TickCount);

		static void Main(string[] args)
		{
			var task1 = new Task(Task1);
			var task2 = new Task(Task2);
			var task3 = new Task(Task3);
			var task4 = new Task(Task4);

			task1.Start();
			task1.Wait();

			task2.Start();
			task2.Wait();

			task3.Start();
			task3.Wait();

			task4.Start();
			task4.Wait();

			Console.ReadKey();
		}

		static void Task1()
		{
			Console.WriteLine("Task1");
			array = new int[10];

			for (int i = 0; i < 10; i++)
			{
				array[i] = random.Next() % 100;
				Console.WriteLine(array[i]);
			}
			Console.WriteLine();
		}

		static void Task2()
		{
			Console.WriteLine("Task2");

			var randomNumber = random.Next() % 100;
			Console.WriteLine("Random number = " + randomNumber);
			for (int i = 0; i < 10; i++)
			{
				array[i] *= randomNumber;
				Console.WriteLine(array[i]);
			}
			Console.WriteLine();
		}

		static void Task3()
		{
			Console.WriteLine("Task3");

			var randomNumber = random.Next() % 100;
			Array.Sort(array);
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(array[i]);
			}
			Console.WriteLine();
		}

		static void Task4()
		{
			Console.WriteLine("Task4");

			Console.WriteLine("Average = " + array.Average());

			Console.WriteLine();
		}
	}
}
