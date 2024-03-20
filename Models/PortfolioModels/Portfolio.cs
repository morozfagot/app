using WebApplication10.Models.ClientModels;
using WebApplication10.Models.PortfolioStockModels;

namespace WebApplication10.Models.PortfolioModels
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int Cash { get; set; }
        public Client? Client { get; set; }
        public int ClientId { get; set; }
        public List<PortfolioStock>? PortfolioStocks { get; set; }
    }
}
