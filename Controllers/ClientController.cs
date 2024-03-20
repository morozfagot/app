using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models.ClientModels;
using WebApplication10.Models.ClientModels.ClientInputModels;
using WebApplication10.Models.ClientModels.ClientViewModels.ClientSuccessViewModels;
using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.PortfolioStockModels;
using WebApplication10.Repositories;

namespace WebApplication10.Controllers
{
    public class ClientController : Controller
    {
        ClientRepository _clientRepository;
        public ClientController(ClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            ViewData["Title"] = "Список клиентов";
            var clients = await _clientRepository.GetClientsAsync();
            var model = clients
                .Select(c => new ClientViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                })
                .ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateInputModel input)
        {
            try
            {
                var model = new Client
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Password = input.Password,
                    Portfolio = new Portfolio
                    {
                        Cash = input.PortfolioCash,
                        PortfolioStocks = new List<PortfolioStock>()
                    }
                };
                await _clientRepository.AddAsync(model);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientEditInputModel input)
        {
            try
            {
                var client = await _clientRepository.GetClientAsync(c => c.Id == input.Id);
                client.FirstName = input.FirstName;
                client.LastName =input.LastName;
                if (_clientRepository.UpdateClient(client))
                {
                    await _clientRepository.SaveAsync();
                    var model = new ClientViewModel
                    {
                        Id = client.Id,
                        FirstName = client.FirstName,
                        LastName = client.LastName
                    };
                    return Ok(model);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
