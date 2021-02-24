using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using StockTicker.Helpers;
using StockTicker.Repositories;
using System;
using System.Threading.Tasks;

namespace StockTicker.Functions
{
    public class GenerateStocks
    {
        private readonly ILogger<GenerateStocks> _logger;
        private readonly IStockRepository _stockRepository;

        public GenerateStocks(
            ILogger<GenerateStocks> logger,
            IStockRepository stockRepository)
        {
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [FunctionName(nameof(GenerateStocks))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GenerateStocks/{number}")] HttpRequest req,
            int number)
        {
            IActionResult result;

            try
            {
                var stocks = StockDataHelper.GenerateStocks(number);

                foreach (var stock in stocks)
                {
                    await _stockRepository.AddStockItem(stock);
                }

                result = new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Whoops. Exception thrown: {ex.Message}");
                result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return result;
        }
    }
}
