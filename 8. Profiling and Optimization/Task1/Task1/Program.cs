using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] salt = new byte[256];
			Random random = new Random();
			random.NextBytes(salt);
			string pas = "Admin12345";

			var res1 = GeneratePasswordHashUsingSalt(pas, salt);
			var res2 = GeneratePasswordHashUsingSaltOptimized1(pas, salt);

			Console.WriteLine(res1 == res2);

			Console.ReadKey();
		}

		public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
		{
			var iterate = 10000;
			var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
			byte[] hash = pbkdf2.GetBytes(20); 

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			var passwordHash = Convert.ToBase64String(hashBytes);

			return passwordHash;
		}

		public static string GeneratePasswordHashUsingSaltOptimized1(string passwordText, byte[] salt)
		{
			var iterate = 10000;
			var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
			byte[] hash = pbkdf2.GetBytes(20); // <- this takes much time but we cannot change it in any way

			byte[] hashBytes = new byte[36];
			Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
			Buffer.BlockCopy(hash, 0, hashBytes, 16, 20);

			var passwordHash = Convert.ToBase64String(hashBytes);

			return passwordHash;
		}
	}
}
