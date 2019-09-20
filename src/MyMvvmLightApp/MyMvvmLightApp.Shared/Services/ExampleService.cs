using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyMvvmLightApp.Services
{
	public interface IExampleService
	{
		void DoSomething();

		Task DoSomethingAsync();
	}

	public class ExampleService : IExampleService
	{
		private readonly ILogger<ExampleService> _logger;
		private readonly HttpClient _httpClient;

		public ExampleService(ILogger<ExampleService> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient("MyHost");
		}

		public void DoSomething()
		{
			_logger.LogDebug($"Doing something {nameof(ExampleService)}.");
		}

		public async Task DoSomethingAsync()
		{
			_logger.LogDebug($"Doing something async {nameof(ExampleService)}.");

			var result = await _httpClient.GetStringAsync("http://google.com");
		}
	}

	public class MockExampleService : IExampleService
	{
		private readonly ILogger<ExampleService> _logger;

		public MockExampleService(ILogger<ExampleService> logger)
		{
			_logger = logger;
		}

		public void DoSomething()
		{
			_logger.LogDebug($"Critical error from {nameof(MockExampleService)}.");
		}

		public Task DoSomethingAsync()
		{
			return Task.CompletedTask;
		}
	}
}
