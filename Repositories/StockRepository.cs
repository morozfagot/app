using WebApplication10.Models.ClientModels;
using WebApplication10.Models.StockModels;

namespace WebApplication10.Repositories
{
    public class StockRepository
    {
        private readonly TradingDbContext _dbContext;
        public StockRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Stock input)
        {
            await _dbContext.Stocks.AddAsync(input);
        }
    }
}
