using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTicker;
using StockTicker.Repositories;
using System.IO;

[assembly: FunctionsStartup(typeof(Startup))]
namespace StockTicker
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddLogging();

            var cosmosClientOptions = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Direct,
                ApplicationRegion = "Australia East",
                AllowBulkExecution = true,
                MaxRetryAttemptsOnRateLimitedRequests = 3
            };

            builder.Services.AddSingleton((s) => new CosmosClient(config["CosmosDBConnectionString"], cosmosClientOptions));

            builder.Services.AddTransient<IStockRepository, StockRepository>();
            builder.Services.AddTransient<IStockAggregateRepository, StockAggregateRepository>();
        }
    }
}
