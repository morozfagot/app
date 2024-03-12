namespace WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioErrorViewModels
{
    public class PortfolioErrorTradeViewModel
    {
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; } = string.Empty;
        public int StockChange { get; set; }
    }
}
