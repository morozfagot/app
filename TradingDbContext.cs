using Microsoft.EntityFrameworkCore;
using WebApplication10.Configurations;
using WebApplication10.Models.ClientModels;
using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.PortfolioStockModels;
using WebApplication10.Models.StockModels;

namespace WebApplication10
{
    public class TradingDbContext : DbContext
    {
        public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options) {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PortfolioStock> PortfoliosStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioStockConfiguration());

            var lukoil = new Stock
            {
                Id = 1,
                Name = "Lukoil",
                Price = 6000
            };
            var yandex = new Stock
            {
                Id = 2,
                Name = "Yandex",
                Price = 3000
            };

            var evgeny = new Client
            {
                Id = 1,
                FirstName = "Евгений",
                LastName = "Морозов",
                Password = "1234",
            };

            var vladimir = new Client
            {
                Id = 2,
                FirstName = "Владимир",
                LastName = "Сапронов",
                Password = "1234",
            };

            var portfolio1 = new Portfolio
            {
                Id = 1,
                Cash = 15000,
                ClientId = evgeny.Id
            };

            var portfolio2 = new Portfolio
            {
                Id = 2,
                Cash = 20000,
                ClientId = vladimir.Id
            };
           
            var stock1 = new PortfolioStock
            {
                Id = 1,
                Count = 5,
                PortfolioId = portfolio1.Id,
                StockId = lukoil.Id,
                TotalPrice = 30000
            };

            var stock2 = new PortfolioStock
            {
                Id = 2,
                Count = 8,
                PortfolioId = portfolio2.Id,
                StockId = yandex.Id,
                TotalPrice = 24000
            };
            var stock3 = new PortfolioStock
            {
                Id = 3,
                Count = 12,
                PortfolioId = portfolio2.Id,
                StockId = lukoil.Id,
                TotalPrice = 72000
            };

            modelBuilder.Entity<Portfolio>().HasData(portfolio1, portfolio2);
            modelBuilder.Entity<Client>().HasData(evgeny, vladimir);
            modelBuilder.Entity<Stock>().HasData(lukoil, yandex);
            modelBuilder.Entity<PortfolioStock>().HasData(stock1, stock2, stock3);
            base.OnModelCreating(modelBuilder);
        }
    }
}
