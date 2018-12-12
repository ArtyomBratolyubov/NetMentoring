using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Services;

namespace WebApplication1.Controllers
{
	public class ValuesController : ApiController
	{
		private IService service;

		public ValuesController(IService service)
		{
			this.service = service;
		}

		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { service.StringGuid() };
		}

		// GET api/values/5
		public int Get(int id)
		{
			return service.Sum(id, 2);
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
