namespace WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel
{
    public class PortfolioTradeViewModel
    {
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; } = string.Empty;
        public int StockCount { get; set; }
        public int StockTotalCount { get; set; }
        public int StockChange { get; set; }
        public int Cash { get; set; }
    }
}
