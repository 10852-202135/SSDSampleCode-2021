using L01SampleAuth.Data;
using L01SampleAuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;

namespace L01SampleAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();//.Run();

            //Initialize app secrets
            var configuration = host.Services.GetService<IConfiguration>();
            var hosting = host.Services.GetService<IWebHostEnvironment>();


            var secrets = configuration.GetSection("Secrets").Get<AppSecrets>();
            DbInitializer.appSecrets = secrets;


            using (var scope = host.Services.CreateScope())
            {
                DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
config.AddAzureKeyVault(
keyVaultEndpoint,
new DefaultAzureCredential());
})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
