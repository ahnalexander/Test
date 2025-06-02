namespace MyApp.Application.Companies.Models;

public class CompanyDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
}
