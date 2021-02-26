using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using StockTicker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Repositories
{
    public class StockAggregateRepository : IStockAggregateRepository
    {
        private readonly IConfiguration _config;
        private readonly CosmosClient _cosmosClient;

        private Container _stockAggregateContainer;

        public StockAggregateRepository(
            IConfiguration config,
            CosmosClient cosmosClient)
        {
            _config = config;
            _cosmosClient = cosmosClient;

            _stockAggregateContainer = _cosmosClient.GetContainer(_config["StockDatabaseName"], _config["StockAggregateContainerName"]);
        }

        public async Task AddStockAggregate(Stock stock)
        {
            ItemRequestOptions itemRequestOptions = new ItemRequestOptions
            {
                EnableContentResponseOnWrite = false
            };

            await _stockAggregateContainer.CreateItemAsync(
                stock,
                new PartitionKey(stock.Index),
                itemRequestOptions);
        }
    }
}
