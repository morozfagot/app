using WebApplication10.Models.PortfolioModels.StockModels.StockViewModels;

namespace WebApplication10.Models.PortfolioModels.StockModel
{
    public class StockDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }

        public static implicit operator StockViewModel(StockDbModel model)
        {
            return new StockViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Count = model.Count,
                TotalCount = model.TotalCount
            };
        }
    }
}
