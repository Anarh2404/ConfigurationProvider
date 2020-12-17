using ConfigurationProvider.MyConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfigurationProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.Sources.Clear();
                    config.AddMyConfiguration();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var asd = hostContext.Configuration.GetSection("Auth");
                    var dsa = asd.GetValue<string>("ServiceUrl");
                    services.AddOptions<MyOption>().BindConfiguration("Auth");
                    services.AddHostedService<Worker>();
                });
    }
}