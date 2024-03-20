using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication10.Models.ClientModels;

namespace WebApplication10.Repositories
{
    public class ClientRepository
    {
        private readonly TradingDbContext _dbContext;
        public ClientRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Client input)
        {
            await _dbContext.Clients.AddAsync(input);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var result = await _dbContext.Clients
                .AsNoTracking()
                .Include(c => c.Portfolio)
                .ThenInclude(p => p.PortfolioStocks)
                .ThenInclude(ps => ps.Stock)
                .ToListAsync();
            return result;
        }
        public async Task<Client> GetClientAsync(Expression<Func<Client, bool>> expression)
        {
            var result = await _dbContext.Clients
                .AsNoTracking()
                .Include(c => c.Portfolio)
                .ThenInclude(p => p.PortfolioStocks)
                .ThenInclude(ps => ps.Stock)
                .SingleAsync(expression);
            return result;
        }
        public bool UpdateClient(Client input)
        {
            try
            {
                _dbContext.Clients.Update(input);
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
