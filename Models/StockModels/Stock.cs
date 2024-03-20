using WebApplication10.Models.PortfolioStockModels;

namespace WebApplication10.Models.StockModels
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<PortfolioStock>? PortfoliosStock { get; set; }
    }
}
