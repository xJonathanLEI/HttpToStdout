using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HttpToStdout
{
    public class Program
    {
        static int Main(string[] args)
        {
            var portOption = new Option(new string[] { "-p", "--port" }, "Port to listen on");
            portOption.Argument = new Argument<int>("port", () => 8080);

            var rootCommand = new RootCommand();
            rootCommand.Description = "A simple .NET tool for printing incoming HTTP requests to console";
            rootCommand.AddOption(portOption);
            rootCommand.Handler = CommandHandler.Create<int>(async (port) =>
            {
                if (port < 0 || port > 65535)
                    throw new ArgumentOutOfRangeException(nameof(port));

                await CreateHostBuilder(new string[] { "--urls", $"http://0.0.0.0:{port}" }).Build().RunAsync();
            });

            // Parses the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                });
    }
}
