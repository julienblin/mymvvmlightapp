using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyMvvmLightApp.Services;
using MyMvvmLightApp.ViewModels;
using Polly;
using Windows.Storage;

namespace MyMvvmLightApp
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; private set; }

		public static void Initialize()
		{
			var configFile = ExtractResource("appsettings.json", ApplicationData.Current.LocalFolder.Path);

			var host = new HostBuilder()
				.ConfigureHostConfiguration(c =>
				{
					c.AddCommandLine(new string[] { $"ContentRoot={ApplicationData.Current.LocalFolder.Path}" });
					c.AddJsonFile(configFile);
				})
				.ConfigureServices(ConfigureServices)
				.ConfigureLogging(l => l.AddConsole())
				.Build();

			ServiceProvider = host.Services;
		}

		private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
		{
			if (ctx.HostingEnvironment.IsEnvironment("Mock"))
			{
				services.AddSingleton<IExampleService, MockExampleService>();
			}
			else
			{
				services.AddSingleton<IExampleService, ExampleService>();
			}

			services.AddTransient<MainPageViewModel>();

			services.AddHttpClient("MyHost", client =>
			{
				client.BaseAddress = new Uri("MyHostUrl");
			})
			.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(1),
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10)
			}));

			var world = ctx.Configuration["Hello"];
		}

		private static string ExtractResource(string resourceName, string targetLocation)
		{
			var assembly = Assembly.GetExecutingAssembly();

			var actualResourceName = assembly
				.GetManifestResourceNames()
				.FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));

			using (var resourceFileStream = assembly.GetManifestResourceStream(actualResourceName))
			{
				if (resourceFileStream != null)
				{
					var full = Path.Combine(targetLocation, resourceName);

					using (var stream = File.Create(full))
					{
						resourceFileStream.CopyTo(stream);
					}
				}
			}

			return Path.Combine(targetLocation, resourceName);
		}
	}
}
