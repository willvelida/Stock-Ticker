namespace StockTicker.Models
{
    public class Stock
    {
        public string StockId { get; set; }
        public string Name { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int NumberOfTrades { get; set; }
        public string Index { get; set; }
    }
}
