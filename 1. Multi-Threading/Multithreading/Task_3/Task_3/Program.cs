using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] matrix1 = new int[,]
			{
				{ 1, 2, 3 },
				{ 4, 5, 6 },
				{ 7, 8, 9 }
			};

			int[,] matrix2 = new int[,]
			{
				{ 1, 2 },
				{ 3, 4 },
				{ 5, 6 }
			};

			MatrixHelper.Display(matrix1);
			Console.WriteLine("     X");
			MatrixHelper.Display(matrix2);
			Console.WriteLine("     =");

			int[,] result = MatrixHelper.Multiply(matrix1, matrix2);

			MatrixHelper.Display(result);

			Console.ReadKey();
		}
	}
}
