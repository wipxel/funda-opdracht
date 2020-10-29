using Funda.Core.Application;
using Funda.Partners.API.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Funda.Workers.AppHost
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
              .ConfigureHostConfiguration(hostConfig =>
              {
                  hostConfig.SetBasePath(Directory.GetCurrentDirectory());
                  hostConfig.AddJsonFile("appsettings.json", optional: true);
                  hostConfig.AddEnvironmentVariables(prefix: "FUNDA_");
                  hostConfig.AddCommandLine(args);
              })

             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.ClearProviders();
                 logging.AddConsole();
             })

              .ConfigureServices((hostContext, services) =>
              {
                  var apiKey = hostContext.Configuration.GetValue<string>("apis:partners:key");

                  services.AddHttpClient("PartnerClient", c =>
                  {
                      c.BaseAddress = new Uri(" http://partnerapi.funda.nl");
                      c.DefaultRequestHeaders.Add("Accept", "application/json");
                      c.DefaultRequestHeaders.Add("User-Agent", "ForqStudio.Assignment.Funda");
                  });

                  services.AddTransient<IPartnerClient>(ctx =>
                  {
                      var clientFactory = ctx.GetRequiredService<IHttpClientFactory>();
                      var httpClient = clientFactory.CreateClient("PartnerClient");
                      return new PartnerClient(httpClient, apiKey);
                  });

                  services.AddTransient<IRealEstateService, RealEstateService>();
                  services.AddHostedService<TopTenAgentsWorker>();
              });
    }
}
