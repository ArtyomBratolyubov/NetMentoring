using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using WebApplication.Services;
using WebApplication1.Aop;

namespace WebApplication1
{
	public static class UnityConfig
	{
		public static void RegisterTypes(HttpConfiguration config)
		{
			var container = new UnityContainer();

			container.AddNewExtension<Interception>();
			container.RegisterType<IService, SomeService>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<LoggingAspect>());

			config.DependencyResolver = new UnityResolver(container);
		}
	}

	public class UnityResolver : IDependencyResolver
	{
		protected IUnityContainer container;

		public UnityResolver(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this.container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = container.CreateChildContainer();
			return new UnityResolver(child);
		}

		public void Dispose()
		{
			container.Dispose();
		}
	}
}