namespace CleanArchitectureReference.Domain.Entities;

public class Restaurant : AuditableEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = [];

    // public User Owner { get; set; } = null!;
    // public string OwnerId { get; set; } = null!;
    // public string? LogoUrl { get; set; }
}