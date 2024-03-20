using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.StockModels;

namespace WebApplication10.Models.PortfolioStockModels
{
    public class PortfolioStock
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int TotalPrice {  get; set; } 
        public int StockId { get; set; }  
        public Stock? Stock { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio? Portfolio { get; set; }
    }
}