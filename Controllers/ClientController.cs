using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models.ClientModels;
using WebApplication10.Models.ClientModels.ClientInputModels;
using WebApplication10.Models.ClientModels.ClientViewModels.ClientSuccessViewModels;

namespace WebApplication10.Controllers
{
    public class ClientController : Controller
    {
        List<ClientDbModel> clients;
        public ClientController(SourceDB sourceDB) 
        {
            clients = sourceDB.Clients;
        }
        [HttpGet]
        public IActionResult List()
        {
            ViewData["Title"] = "Список клиентов";
            var model = clients.Select(x => new ClientViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).OrderBy(x => x.Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ClientEditInputModel input)
        {
            try
            {
                var client = clients.Single(x => x.Id == input.Id);
                var clientNew = new ClientDbModel
                {
                    Id = client.Id,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Password = client.Password,
                    PortfolioDbModel = client.PortfolioDbModel
                };

                clients.Remove(client);
                clients.Add(clientNew);

                var model = new ClientViewModel
                {
                    Id = clientNew.Id,
                    FirstName = clientNew.FirstName,
                    LastName = clientNew.LastName,
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
