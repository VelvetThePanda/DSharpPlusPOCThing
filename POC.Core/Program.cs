using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Templates;

namespace POC.Core
{
	public class Program
	{
		public static async Task Main()
		{
			var host = Host.CreateDefaultBuilder();

			host.ConfigureAppConfiguration((_, configuration) =>
			{
				configuration.SetBasePath(Directory.GetCurrentDirectory());
				configuration.AddUserSecrets("17047783-b481-4a1b-b681-dad97db232bd", false);
			});
			
			host.ConfigureServices((c, s) =>
			{
				s.AddLogging(l => l.ClearProviders().AddSerilog());

				Log.Logger = new LoggerConfiguration()
					.MinimumLevel.Debug()
					.WriteTo.Console(new ExpressionTemplate("[{@t:h:mm:ss ff tt}] [{@l:u3}]{#if EventId is not null} [{EventId.Name}]{#end} [{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m}\n{@x}"))
					.CreateLogger();
	
				Log.Information("Logger configured");
				
				var config = new DiscordConfiguration
				{
					Token = c.Configuration.GetValue<string>("Discord"),
					TokenType = TokenType.Bot,
					AutoReconnect = true,
					MinimumLogLevel= LogLevel.Debug
				};
	
				var rest = new DiscordRestClient(config);
				var client = new DiscordClient(config);
	
				s.AddSingleton<DiscordRestClient>(rest);
				s.AddSingleton<DiscordClient>(client);

				
			});

			await host.UseConsoleLifetime().RunConsoleAsync();
		}
	}
}