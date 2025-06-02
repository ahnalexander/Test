using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using System.Security.Cryptography;

namespace MyApp.Infrastructure;

public static class DatabaseSeeder
{
    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        const int iterations = 100000;

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); 

        return $"{iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!await context.Users.AnyAsync())
        {
            var testUser = new User
            {
                Username = "string",
                Email = "test@example.com",
                PasswordHash = HashPassword("string"),
                Role = "Admin"
            };
            context.Users.Add(testUser);
        }

        if (!await context.Companies.AnyAsync())
        {
            var companies = new List<Company>
            {
                new() { Name = "Company A", Address = "Address A" },
                new() { Name = "Company B", Address = "Address B" }
            };
            context.Companies.AddRange(companies);
        }

        if (!await context.Employees.AnyAsync())
        {
            var employees = new List<Employee>
            {
                new() { Name = "John Doe", Email = "john@example.com", CompanyId = 1 },
                new() { Name = "Jane Smith", Email = "jane@example.com", CompanyId = 1 },
                new() { Name = "Bob", Email = "Bob@example.com" , CompanyId = 2}
            };
            context.Employees.AddRange(employees);
        }

        await context.SaveChangesAsync();
    }
}