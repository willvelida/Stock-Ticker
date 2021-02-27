using StockTicker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Repositories
{
    public interface IStockAggregateRepository
    {
        /// <summary>
        /// Adds a stock document to the Aggregate container
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        Task AddStockAggregate(Stock stock);
    }
}
