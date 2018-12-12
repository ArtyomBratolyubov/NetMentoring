using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication1.Aop
{
	public class LoggingAspect : IInterceptionBehavior
	{
		public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
		{
			List<string> args = new List<string>();
			foreach (var arg in input.Arguments)
			{
				try
				{
					XmlSerializer xsSubmit = new XmlSerializer(arg.GetType());

					using (var sww = new StringWriter())
					{
						using (XmlWriter writer = XmlWriter.Create(sww))
						{
							xsSubmit.Serialize(writer, arg);
							args.Add(sww.ToString());
						}
					}
				}
				catch (Exception e)
				{
					args.Add("Not serializable");
					throw;
				}
			}

			WriteLog(String.Format("Invoking method {0} at {1} with arguments: {2}", 
				input.MethodBase, 
				DateTime.Now.ToLongTimeString(),
				string.Join(" ", args)));

			var result = getNext()(input, getNext);
			if (result.Exception != null)
			{
				WriteLog(String.Format("Method {0} threw exception {1} at {2}", input.MethodBase, result.Exception.Message, DateTime.Now.ToString()));
			}
			else
			{
				WriteLog(String.Format("Method {0} returned {1} at {2}", 
					input.MethodBase, 
					result.ReturnValue, 
					DateTime.Now.ToString()));
			}

			return result;
		}

		public IEnumerable<Type> GetRequiredInterfaces()
		{
			return Type.EmptyTypes;
		}

		public bool WillExecute { get { return true; } }

		private void WriteLog(string message)
		{
			System.Diagnostics.Debug.WriteLine("LOG:" + message);
		}
	}
}