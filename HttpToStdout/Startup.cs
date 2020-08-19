using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpToStdout
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/{**any}", async context =>
                {
                    string bodyText;
                    if (context.Request.ContentLength > 0)
                    {
                        using (var bodySR = new StreamReader(context.Request.Body, Encoding.UTF8))
                            bodyText = await bodySR.ReadToEndAsync();
                    }
                    else
                    {
                        bodyText = null;
                    }

                    Console.WriteLine($"Request from {context.Connection.RemoteIpAddress}:");
                    Console.WriteLine();

                    Console.WriteLine($"{context.Request.Protocol} {context.Request.Method.ToUpper()} {context.Request.Path}{context.Request.QueryString}");
                    foreach (var header in context.Request.Headers)
                        Console.WriteLine($"{header.Key}: {header.Value}");

                    if (!string.IsNullOrEmpty(bodyText))
                    {
                        Console.WriteLine();
                        Console.WriteLine(bodyText);
                    }

                    Console.WriteLine();

                    context.Response.StatusCode = (int)StatusCodes.Status204NoContent;
                });
            });
        }
    }
}
