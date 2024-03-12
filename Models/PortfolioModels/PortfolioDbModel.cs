using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel;
using WebApplication10.Models.PortfolioModels.StockModel;
using WebApplication10.Models.PortfolioModels.StockModels.StockViewModels;

namespace WebApplication10.Models.PortfolioModels
{
    public class PortfolioDbModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<StockDbModel> Stocks { get; set; } = new List<StockDbModel>();
        public int Cash { get; set; }
        
        public static implicit operator PortfolioViewModel(PortfolioDbModel model)
        {
            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = model.Id,
                ClientId = model.ClientId,
                Cash = model.Cash
            };
            foreach (var item in model.Stocks)
            {
                portfolioViewModel.Stocks.Add((StockViewModel)item);
            }
            return portfolioViewModel;
        }
        public static implicit operator PortfolioWithrawalViewModel(PortfolioDbModel model)
        {
            return new PortfolioWithrawalViewModel { Id = model.Id, Cash = model.Cash };
        } 
    }
}
