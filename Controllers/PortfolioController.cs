using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models.StockModels.StockViewModels;
using WebApplication10.Models.PortfolioModels.PortfolioInputModels;
using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioErrorViewModels;
using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel;
using WebApplication10.Repositories;

namespace WebApplication10.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ClientRepository _clientRepository;
        private readonly PortfolioRepository _portfolioRepository;
        public PortfolioController(ClientRepository clientRepository, PortfolioRepository portfolioRepository)
        {
            var array = new int[3] { 1,2,3 };
            _clientRepository = clientRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List(int id)
        {
            ViewData["Title"] = "Акции";
            var client = await _clientRepository.GetClientAsync(c => c.Id == id);
            var model = new ClientPortfolioViewModel
            {
                Id = id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Portfolio = new PortfolioViewModel
                {
                    Id = client.Portfolio.Id,
                    Cash = client.Portfolio.Cash,
                    ClientId = client.Id,
                    Stocks = client.Portfolio.PortfolioStocks
                    .Select(ps => new StockViewModel {
                        Id = ps.StockId,
                        Name = ps.Stock.Name,
                        Count = ps.Count,
                        Price = ps.Stock.Price,
                        TotalCount = ps.TotalPrice
                    })
                    .ToList()
                }
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Buy(PortfolioTradeInputModel input)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(x => x.Id == input.Id);
            var portfolioStock = portfolio.PortfolioStocks.Single(ps => ps.StockId == input.StockId);
            if (portfolio.Cash >= input.Count * portfolioStock.Stock.Price)
            {
                portfolioStock.Count += input.Count;
                portfolioStock.TotalPrice = portfolioStock.Count * portfolioStock.Stock.Price;
                portfolio.Cash -= input.Count * portfolioStock.Stock.Price;
                if (_portfolioRepository.UpdatePortfolio(portfolio))
                {
                    await _portfolioRepository.SaveAsync();
                    var model = new PortfolioTradeViewModel
                    {
                        PortfolioId = portfolio.Id,
                        StockId = portfolioStock.Stock.Id,
                        StockName = portfolioStock.Stock.Name,
                        StockCount = portfolioStock.Count,
                        StockTotalCount = portfolioStock.TotalPrice,
                        StockChange = input.Count,
                        Cash = portfolio.Cash
                    };
                    return Ok(model);
                }
                else
                {
                    var error = new PortfolioErrorTradeViewModel
                    {
                        StockId = portfolioStock.Stock.Id,
                        StockName = portfolioStock.Stock.Name,
                        PortfolioId = portfolio.Id,
                        StockChange = input.Count
                    };
                    return UnprocessableEntity(error);
                }
               
            }
            else
            {
                var error = new PortfolioErrorTradeViewModel
                {
                    StockId = portfolioStock.Stock.Id,
                    StockName = portfolioStock.Stock.Name,
                    PortfolioId = portfolio.Id,
                    StockChange = input.Count
                };
                return UnprocessableEntity(error);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Sell(PortfolioTradeInputModel input)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(x => x.Id == input.Id);
            var portfolioStock = portfolio.PortfolioStocks.Single(ps => ps.StockId == input.StockId);

            if (portfolioStock.Count >= input.Count)
            {
                portfolioStock.Count -= input.Count;
                portfolioStock.TotalPrice = portfolioStock.Count * portfolioStock.Stock.Price;
                portfolio.Cash += input.Count * portfolioStock.Stock.Price;
                if (_portfolioRepository.UpdatePortfolio(portfolio))
                {
                    await _portfolioRepository.SaveAsync();
                    var model = new PortfolioTradeViewModel
                    {
                        PortfolioId = portfolio.Id,
                        StockId = portfolioStock.Stock.Id,
                        StockName = portfolioStock.Stock.Name,
                        StockCount = portfolioStock.Count,
                        StockTotalCount = portfolioStock.TotalPrice,
                        StockChange = input.Count,
                        Cash = portfolio.Cash
                    };
                    return Ok(model);
                }
                else
                {
                    var error = new PortfolioErrorTradeViewModel
                    {
                        StockId = portfolioStock.Stock.Id,
                        StockName = portfolioStock.Stock.Name,
                        PortfolioId = portfolio.Id,
                        StockChange = input.Count
                    };
                    return UnprocessableEntity(error);
                }

            }
            else
            {
                var error = new PortfolioErrorTradeViewModel
                {
                    StockId = portfolioStock.Stock.Id,
                    StockName = portfolioStock.Stock.Name,
                    PortfolioId = portfolio.Id,
                    StockChange = input.Count
                };
                return UnprocessableEntity(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Withraw(PortfolioWithrawalInputModel input)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(p => p.Id == input.Id);
            if (portfolio.Cash >= input.Amount)
            {
                portfolio.Cash -= input.Amount;
                if(_portfolioRepository.UpdatePortfolio(portfolio))
                {
                    await _portfolioRepository.SaveAsync();
                    var model = new PortfolioWithrawalViewModel
                    {
                        Id = portfolio.Id,
                        Cash = portfolio.Cash,
                        CashChange = input.Amount
                    };
                    return Ok(model);
                }else
                {
                    return UnprocessableEntity();
                }
            }
            else {
                return UnprocessableEntity();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(PortfolioWithrawalInputModel input)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(p => p.Id == input.Id);
            portfolio.Cash += input.Amount;
            if(_portfolioRepository.UpdatePortfolio(portfolio))
            {
                await _portfolioRepository.SaveAsync();
                var model = new PortfolioWithrawalViewModel
                {
                    Id = portfolio.Id,
                    Cash = portfolio.Cash,
                    CashChange = input.Amount
                };
                return Ok(model);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}
