using WebApplication10.Models.PortfolioModels.StockModels.StockViewModels;

namespace WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<StockViewModel> Stocks { get; set; } = new List<StockViewModel>();
        public int Cash { get; set; }
    }
}
