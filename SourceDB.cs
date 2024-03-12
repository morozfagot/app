using WebApplication10.Models.ClientModels;
using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.PortfolioModels.StockModel;

namespace WebApplication10
{
    public class SourceDB
    {
        private List<ClientDbModel> clients;
        private List<PortfolioDbModel> portfolios;
        public List<ClientDbModel> Clients { get { return clients; } }
        public List<PortfolioDbModel> Portfolios { get { return portfolios; } }
        public SourceDB()
        {
            portfolios = new List<PortfolioDbModel>
            {
                new PortfolioDbModel
                {
                    Id = 1,
                    ClientId = 1,
                    Stocks = new List<StockDbModel>
                    {
                        new StockDbModel
                        {
                            Id = 1,
                            Name = "Lukoil",
                            Price = 6000,
                            Count = 3,
                            TotalCount = 18000
                        },
                        new StockDbModel
                        {
                            Id = 2,
                            Name = "Yandex",
                            Price = 3000,
                            Count = 10,
                            TotalCount = 30000
                        }
                    },
                    Cash = 2000
                },
                new PortfolioDbModel
                {
                    Id = 2,
                    ClientId= 2,
                    Stocks = new List<StockDbModel>
                    {
                        new StockDbModel
                        {
                            Id = 2,
                            Name = "Yandex",
                            Price = 3000,
                            Count = 20,
                            TotalCount = 60000
                        }
                    },
                    Cash = 5000
                }

            };
            clients = new List<ClientDbModel>
            {
                new ClientDbModel
                {
                    Id = 1,
                    FirstName = "Евгений",
                    LastName = "Морозов",
                    Password = "123",
                    PortfolioDbModel = portfolios.Single(x => x.Id == 1)
                },
                new ClientDbModel
                {
                    Id = 2,
                    FirstName = "Владимир",
                    LastName = "Сапронов",
                    Password = "234",
                    PortfolioDbModel = portfolios.Single(x =>x.Id == 2)
                }
            };
        }

    }  
}
 