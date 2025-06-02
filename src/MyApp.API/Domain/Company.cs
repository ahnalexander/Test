namespace MyApp.Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}