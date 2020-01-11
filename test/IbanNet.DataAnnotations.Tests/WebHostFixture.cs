#if ASPNET_INTEGRATION_TESTS
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace IbanNet.DataAnnotations
{
	public abstract class WebHostFixture : IDisposable
	{
		public TestServer TestServer { get; private set; }

		protected WebHostFixture()
		{
		}

		public void Start()
		{
			IWebHostBuilder webHostBuilder = new WebHostBuilder();
			Configure(webHostBuilder);
			TestServer = new TestServer(webHostBuilder);
		}

		protected virtual void Configure(IWebHostBuilder webHostBuilder)
		{
		}

		public void Dispose()
		{
			TestServer?.Dispose();
		}

		public abstract IDictionary<string, string[]> MapToErrors(string jsonContent);
	}
}
#endif
