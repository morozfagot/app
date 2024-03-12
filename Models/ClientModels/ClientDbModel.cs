using WebApplication10.Models.ClientModels.ClientViewModels.ClientSuccessViewModels;
using WebApplication10.Models.PortfolioModels;
using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel;

namespace WebApplication10.Models.ClientModels
{
    public class ClientDbModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public PortfolioDbModel PortfolioDbModel { get; set; } = null;
        public static implicit operator ClientViewModel(ClientDbModel clientDbModel)
        {
            return new ClientViewModel
            {
                Id = clientDbModel.Id,
                FirstName = clientDbModel.FirstName,
                LastName = clientDbModel.LastName
            };
        }
        public static implicit operator ClientPortfolioViewModel(ClientDbModel clientDbModel)
        {
            return new ClientPortfolioViewModel
            {
                Id = clientDbModel.Id,
                FirstName = clientDbModel.FirstName,
                LastName = clientDbModel.LastName,
                PortfolioViewModel = (PortfolioViewModel)clientDbModel.PortfolioDbModel
            };
        }
    }
}
