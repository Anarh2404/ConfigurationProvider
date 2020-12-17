using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigurationProvider
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<MyOption> _optionsMonitor;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IOptionsMonitor<MyOption> optionsMonitor)
        {
            _logger = logger;
            _configuration = configuration;
            _optionsMonitor = optionsMonitor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var configValue = _configuration.GetValue<string>("Auth:ServiceUrl");
                var optionValue = _optionsMonitor.CurrentValue.ServiceUrl;
                _logger.LogInformation("Config: " + configValue);
                _logger.LogInformation("Option: " + optionValue);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}