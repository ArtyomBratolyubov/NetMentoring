using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	public class CursorHelper
	{
		private int top;
		private int left;

		public void SaveState()
		{
			this.top = Console.CursorTop;
			this.left = Console.CursorLeft;
		}

		public void LoadState()
		{
			Console.CursorTop = this.top;
			Console.CursorLeft = this.left;
		}

		public static void Set(int left, int top)
		{
			Console.CursorTop = top;
			Console.CursorLeft = left;
		}

		public static void ClearLine(int top)
		{
			Console.CursorTop = top;
			Console.CursorLeft = 0;
			Console.Write(new string(' ', Console.WindowWidth));
		}
	}
}
