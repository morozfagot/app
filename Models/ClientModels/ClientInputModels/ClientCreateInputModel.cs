namespace WebApplication10.Models.ClientModels.ClientInputModels
{
    public class ClientCreateInputModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PortfolioCash { get; set; }
    }
}
