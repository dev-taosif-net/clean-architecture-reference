namespace CleanArchitectureReference.Domain.Entities;

public class Dish : AuditableEntity<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }

    public int RestaurantId { get; set; }
}