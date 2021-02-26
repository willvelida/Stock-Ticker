using Bogus;
using StockTicker.Models;
using System;
using System.Collections.Generic;

namespace StockTicker.Helpers
{
    public static class StockDataHelper
    {
        /// <summary>
        /// Helper method to generate fake stocks.
        /// </summary>
        /// <param name="numberOfStocks"></param>
        /// <returns></returns>
        public static List<Stock> GenerateStocks(int numberOfStocks)
        {
            var stocks = new Faker<Stock>()
                .RuleFor(i => i.StockId, (fake) => Guid.NewGuid().ToString())
                .RuleFor(i => i.Name, (fake) => fake.PickRandom(new List<string> { "MSFT", "GameStop", "Reddit", "Nokia", "Samsung", "Coca-Cola", "Pepsi", "Wrigley", "Dell" }))
                .RuleFor(i => i.OpenPrice, (fake) => fake.Random.Decimal(10.0m, 12.0m))
                .RuleFor(i => i.HighPrice, (fake) => fake.Random.Decimal(25.0m, 30.0m))
                .RuleFor(i => i.LowPrice, (fake) => fake.Random.Decimal(8.0m, 10.0m))
                .RuleFor(i => i.ClosePrice, (fake) => fake.Random.Decimal(11.0m, 30.0m))
                .RuleFor(i => i.NumberOfTrades, (fake) => fake.Random.Int(1, 1000))
                .RuleFor(i => i.Index, (fake) => fake.PickRandom(new List<string> { "S&P 500", "FTSE 100", "Nasdaq", "Nikkei", "NZX", "Russel 2000", "Dow Jones" }))
                .Generate(numberOfStocks);

            return stocks;
        }
    }
}
