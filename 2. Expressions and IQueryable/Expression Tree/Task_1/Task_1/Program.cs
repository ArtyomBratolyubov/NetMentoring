using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Task_1
{
	class Program
	{
		static void Main(string[] args)
		{
			Expression<Func<int, int, int>> source_exp = (a, b) => a + (a + 1) * (b + 5) * (a - 1);

			Console.WriteLine("Before:");
			Console.WriteLine(source_exp + " = " + source_exp.Compile().Invoke(3, 7));

			// Increment transform
			var result_exp = new IncrementTransform().VisitAndConvert(source_exp, "task 1");
			Console.WriteLine("\nAfter increment/decrement transform:");
			Console.WriteLine(result_exp + " = " + result_exp.Compile().Invoke(3, 7));
			
			// Parameter transform
			var map = new Dictionary<string, int>();
			map.Add("b", 12);

			result_exp = new ParametersTransform(map).VisitAndConvert(result_exp, "task 1");
			Console.WriteLine("\nAfter parameter transform:");
			Console.WriteLine(result_exp + " = " + result_exp.Compile().Invoke(3, 7));

			Console.ReadKey();
		}
	}
}
