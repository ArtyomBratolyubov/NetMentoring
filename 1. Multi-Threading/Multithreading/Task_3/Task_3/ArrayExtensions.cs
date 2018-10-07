using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
	public static class ArrayExtensions
	{
		public static int Cols(this int[,] array)
		{
			return array.GetLength(1);
		}

		public static int Rows(this int[,] array)
		{
			return array.GetLength(0);
		}
	}
}
