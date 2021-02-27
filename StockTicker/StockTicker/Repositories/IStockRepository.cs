using StockTicker.Models;
using System.Threading.Tasks;

namespace StockTicker.Repositories
{
    public interface IStockRepository
    {
        /// <summary>
        /// Adds a stock to the Stock collection
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        Task AddStockItem(Stock stock);
    }
}
