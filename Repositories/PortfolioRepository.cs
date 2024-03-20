using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication10.Models.ClientModels;
using WebApplication10.Models.PortfolioModels;

namespace WebApplication10.Repositories
{
    public class PortfolioRepository
    {
        private readonly TradingDbContext _dbContext;
        public PortfolioRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Portfolio input)
        {
            await _dbContext.Portfolios.AddAsync(input);
        }

        public async Task<Portfolio> GetPortfolioAsync(Expression<Func<Portfolio, bool>> expression)
        {
            var result = await _dbContext.Portfolios
                .AsNoTracking()
                .Include(p => p.PortfolioStocks)
                .ThenInclude(ps =>ps.Stock)
                .SingleAsync(expression);
            return result;
        }

        public bool UpdatePortfolio(Portfolio input)
        {
            try
            {
                _dbContext.Portfolios.Update(input);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                int result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
