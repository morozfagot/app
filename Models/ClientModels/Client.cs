using WebApplication10.Models.PortfolioModels;

namespace WebApplication10.Models.ClientModels
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Portfolio? Portfolio { get; set; }        
    }
}
