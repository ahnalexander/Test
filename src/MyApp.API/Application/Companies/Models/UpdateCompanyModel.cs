namespace MyApp.Application.Companies.Models;

public record UpdateCompanyModel
{
    public required string Name { get; init; }
    public required string Address { get; init; }
}
