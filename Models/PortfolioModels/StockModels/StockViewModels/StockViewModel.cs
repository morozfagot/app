namespace WebApplication10.Models.PortfolioModels.StockModels.StockViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
