using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models.ClientModels;
using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.PortfolioModels.PortfolioInputModels;
using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioErrorViewModels;
using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel;

namespace WebApplication10.Controllers
{
    public class PortfolioController : Controller
    {
        List<ClientDbModel> clientsDbModel;
        List<PortfolioDbModel> portfoliosDbModel;
        public PortfolioController(SourceDB sourceDB)
        {
            clientsDbModel = sourceDB.Clients;
            portfoliosDbModel = sourceDB.Portfolios;
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            ViewData["Title"] = "Акции";
            ClientPortfolioViewModel clientPortfolioViewModel = (ClientPortfolioViewModel)clientsDbModel.Single(x => x.Id == id);
            return View(clientPortfolioViewModel);
        }
        [HttpPost]
        public IActionResult Buy(PortfolioTradeInputModel input)
        {
            var portfolio = portfoliosDbModel.Single(x => x.Id == input.Id);
            var stock = portfolio.Stocks.Single(x => x.Id == input.StockId);
            if (portfolio.Cash >= input.Count * stock.Price)
            {
                stock.Count += input.Count;
                stock.TotalCount = stock.Count * stock.Price;
                portfolio.Cash -= input.Count * stock.Price;
                var model = new PortfolioTradeViewModel
                {
                    PortfolioId = portfolio.Id,
                    StockId = stock.Id,
                    StockName = stock.Name,
                    StockCount = stock.Count,
                    StockTotalCount = stock.TotalCount,
                    StockChange = input.Count,
                    Cash = portfolio.Cash
                };
                return Ok(model);
            }
            else
            {
                var error = new PortfolioErrorTradeViewModel
                {
                    StockId = stock.Id,
                    StockName = stock.Name,
                    PortfolioId = portfolio.Id,
                    StockChange = input.Count
                };
                return UnprocessableEntity(error);
            }
        }
        [HttpPost]
        public IActionResult Sell(PortfolioTradeInputModel input)
        {
            var portfolio = portfoliosDbModel.Single(x => x.Id == input.Id);
            var stock = portfolio.Stocks.Single(x => x.Id == input.StockId);
            if (stock.Count >= input.Count)
            {
                stock.Count -= input.Count;
                stock.TotalCount = stock.Count * stock.Price;
                portfolio.Cash += input.Count * stock.Price;
                var model = new PortfolioTradeViewModel
                {
                    PortfolioId = portfolio.Id,
                    StockId = stock.Id,
                    StockName = stock.Name,
                    StockCount = stock.Count,
                    StockTotalCount = stock.TotalCount,
                    StockChange = input.Count,
                    Cash = portfolio.Cash
                };
                return Ok(model);
            }
            else
            {
                var error = new PortfolioErrorTradeViewModel
                {
                    StockId =stock.Id,
                    StockName = stock.Name,
                    PortfolioId = portfolio.Id,
                    StockChange = input.Count
                };
                return UnprocessableEntity(error);
            }
        }

        [HttpPost]
        public IActionResult Withraw(PortfolioWithrawalInputModel input)
        {
            var portfolio = portfoliosDbModel.Single(x => x.Id == input.Id);
            if (portfolio.Cash >= input.Amount)
            {
                portfolio.Cash -= input.Amount;
                var model = new PortfolioWithrawalViewModel
                {
                    Id = portfolio.Id,
                    Cash = portfolio.Cash,
                    CashChange = input.Amount
                };
                return Ok(model);
            }
            else {
                return UnprocessableEntity();
            }
        }
        [HttpPost]
        public IActionResult Deposit(PortfolioWithrawalInputModel input)
        {
            var portfolio = portfoliosDbModel.Single(x => x.Id == input.Id);
            portfolio.Cash += input.Amount;
            var model = new PortfolioWithrawalViewModel
            {
                Id = portfolio.Id,
                Cash = portfolio.Cash,
                CashChange = input.Amount
            };
            return Ok(model);
        }
    }
}
