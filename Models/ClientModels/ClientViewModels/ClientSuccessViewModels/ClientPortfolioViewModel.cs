using WebApplication10.Models.PortfolioModels.PortfolioViewModels.PortfolioSuccessViewModel;

public class ClientPortfolioViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public PortfolioViewModel Portfolio { get; set; } = null;
} 
