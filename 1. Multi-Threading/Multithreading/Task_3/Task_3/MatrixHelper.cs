using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
	public static class MatrixHelper
	{
		public static void Display(int[,] matrix)
		{
			for (int i = 0; i < matrix.Rows(); i++)
			{
				for (int j = 0; j < matrix.Cols(); j++)
				{
					Console.Write("{0,4}", matrix[i, j]);
				}

				Console.WriteLine();
			}
		}

		public static int[,] Multiply(int[,] matrix1, int[,] matrix2)
		{
			if (matrix1.Cols() != matrix2.Rows())
			{
				throw new InvalidOperationException("Error");
			}

			int[,] result = new int[matrix1.Rows(), matrix2.Cols()];

			Parallel.For(0, result.Length, (index) => { Calculate(index, matrix1, matrix2, result); });

			return result;
		}

		private static void Calculate(int index, int[,] matrix1, int[,] matrix2, int[,] resultMatrix)
		{
			int[] i2d = ConvertTo2dIndex(index, resultMatrix.Cols());

			int result = 0;
			for (int i = 0; i < matrix1.Cols(); i++)
			{
				result += matrix1[i2d[0], i] * matrix2[i, i2d[1]];
			}

			resultMatrix[i2d[0], i2d[1]] = result;
		}

		private static int[] ConvertTo2dIndex(int index, int cols)
		{
			int row = index / cols;
			int col = index % cols;

			return new int[] { row, col };
		}
	}
}
