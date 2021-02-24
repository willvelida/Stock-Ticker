using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using StockTicker.Models;
using System.Threading.Tasks;

namespace StockTicker.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IConfiguration _config;
        private readonly CosmosClient _cosmosClient;

        private Container _stockContainer;

        public StockRepository(
            IConfiguration config,
            CosmosClient cosmosClient)
        {
            _config = config;
            _cosmosClient = cosmosClient;

            _stockContainer = _cosmosClient.GetContainer(_config["StockDatabaseName"], _config["StockContainerName"]);
        }

        public async Task AddStockItem(Stock stock)
        {
            ItemRequestOptions itemRequestOptions = new ItemRequestOptions
            {
                EnableContentResponseOnWrite = false
            };

            await _stockContainer.CreateItemAsync(
                stock,
                new PartitionKey(stock.StockId),
                itemRequestOptions);
        }
    }
}
