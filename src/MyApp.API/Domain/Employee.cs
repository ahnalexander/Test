namespace MyApp.Domain
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int CompanyId { get; set; }
        public string Email { get; set; } = default!;
        public Company Company { get; set; } = default!;

    }
}