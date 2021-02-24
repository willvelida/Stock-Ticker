using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace StockTicker.Functions
{
    public class SplitStockByIndex
    {
        private readonly ILogger<SplitStockByIndex> _logger;

        [FunctionName(nameof(SplitStockByIndex))]
        public void Run([CosmosDBTrigger(
            databaseName: "StockDBName",
            collectionName: "StockContainerName",
            ConnectionStringSetting = "CosmosDBConnectionString",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input)
        {
            // If we have documents in our list
            if (input != null && input.Count > 0)
            {
                // Foreach document in the list
                foreach (var document in input)
                {
                    // Do something
                }
            }
        }
    }
}
