using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockTicker.Models;
using StockTicker.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTicker.Functions
{
    public class SplitStockByIndex
    {
        private readonly ILogger<SplitStockByIndex> _logger;
        private readonly IStockAggregateRepository _stockAggregateRepository;

        public SplitStockByIndex(
            ILogger<SplitStockByIndex> logger,
            IStockAggregateRepository stockAggregateRepository)
        {
            _logger = logger;
            _stockAggregateRepository = stockAggregateRepository;
        }


        [FunctionName(nameof(SplitStockByIndex))]
        public async Task Run([CosmosDBTrigger(
            databaseName: "StocksDB",
            collectionName: "Stock",
            ConnectionStringSetting = "CosmosDBConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input)
        {
            try
            {
                // If we have documents in our list
                if (input != null && input.Count > 0)
                {
                    // Foreach document in the list
                    foreach (var document in input)
                    {
                        var materializedStockReading = JsonConvert.DeserializeObject<Stock>(document.ToString());

                        await _stockAggregateRepository.AddStockAggregate(materializedStockReading);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong. Exception thrown: {ex.Message}");
                throw;
            }
            
        }
    }
}
