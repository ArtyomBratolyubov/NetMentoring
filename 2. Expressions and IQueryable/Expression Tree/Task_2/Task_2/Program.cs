using System;
using System.Linq.Expressions;
using Task_2.Models;

namespace Task_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var mapGenerator = new MappingGenerator();
			var mapper = mapGenerator.Generate<ClientModel, PersonModel>();

			var client = new ClientModel
			{
				FullName = "Test"
			};
			Console.WriteLine("Client.Fullname = " + client.FullName);
			PersonModel person = mapper.Map(client);

			Console.WriteLine("Person.Name = " + person.Name);
			Console.ReadKey();
		}
	}

	
}
