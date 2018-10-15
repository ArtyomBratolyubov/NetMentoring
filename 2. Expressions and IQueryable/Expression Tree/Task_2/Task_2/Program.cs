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

	public class Mapper<TSource, TDestination>
	{
		Func<TSource, TDestination> mapFunction;

		internal Mapper(Func<TSource, TDestination> func)
		{
			mapFunction = func;
		}

		public TDestination Map(TSource source)
		{
			return mapFunction(source);
		}
	}

	public class MappingGenerator
	{
		public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
		{
			var createdType = typeof(TDestination);

			var sourceParam = Expression.Parameter(typeof(TSource));
			var sourceProp = Expression.Property(sourceParam, "FullName");

			var destValueProperty = createdType.GetProperty("Name");
			var displayValueAssignment = Expression.Bind(destValueProperty, sourceProp);

			var ctor = Expression.New(createdType);
			var memberInit = Expression.MemberInit(ctor, displayValueAssignment);

			var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(memberInit, sourceParam);
			Console.WriteLine(mapFunction);
			return new Mapper<TSource, TDestination>(mapFunction.Compile());
		}
	}
}
