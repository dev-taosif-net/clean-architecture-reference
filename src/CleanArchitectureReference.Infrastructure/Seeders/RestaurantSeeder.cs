using CleanArchitectureReference.Domain.ValueObjects;
using CleanArchitectureReference.Infrastructure.Persistence;

namespace CleanArchitectureReference.Infrastructure.Seeders;

public class RestaurantSeeder(ApplicationDbContext dbContext):IRestaurantSeeder
{

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.Restaurants.AnyAsync(cancellationToken))
        {
            return;
        }

        var restaurants = GetRestaurants();

        dbContext.Restaurants.AddRange(restaurants);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static List<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new Restaurant
            {
                Name = "The Great Restaurant",
                Description = "A great place to eat delicious food.",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "contact@greatrestaurant.com",
                ContactNumber = "+1-202-555-0101",
                Address = new Address("New York", "123 Main St", "10001"),
                Dishes =
                [
                    new Dish
                    {
                        Name = "Spaghetti Carbonara",
                        Description = "Classic Italian pasta dish with eggs, cheese, pancetta, and pepper.",
                        Price = 12.99m
                    },
                    new Dish
                    {
                        Name = "Margherita Pizza",
                        Description = "Traditional pizza with tomato sauce, mozzarella cheese, and fresh basil.",
                        Price = 10.99m
                    }
                ]
            },
            new Restaurant
            {
                Name = "Sushi Palace",
                Description = "Fresh sushi and authentic Japanese cuisine.",
                Category = "Japanese",
                HasDelivery = true,
                ContactEmail = "hello@sushipalace.com",
                ContactNumber = "+1-202-555-0102",
                Address = new Address("San Francisco", "456 Ocean Ave", "94101"),
                Dishes =
                [
                    new Dish
                    {
                        Name = "Salmon Nigiri",
                        Description = "Hand-pressed rice topped with fresh salmon.",
                        Price = 8.50m
                    },
                    new Dish
                    {
                        Name = "California Roll",
                        Description = "Crab, avocado, and cucumber rolled in seaweed and rice.",
                        Price = 7.25m
                    }
                ]
            },
            new Restaurant
            {
                Name = "Taco Fiesta",
                Description = "Vibrant Mexican street food and margaritas.",
                Category = "Mexican",
                HasDelivery = false,
                ContactEmail = "info@tacofiesta.com",
                ContactNumber = "+1-202-555-0103",
                Address = new Address("Austin", "789 Sunset Blvd", "73301"),
                Dishes =
                [
                    new Dish
                    {
                        Name = "Carne Asada Tacos",
                        Description = "Grilled steak tacos with onions, cilantro, and lime.",
                        Price = 9.99m
                    },
                    new Dish
                    {
                        Name = "Chicken Quesadilla",
                        Description = "Grilled tortilla filled with chicken and melted cheese.",
                        Price = 8.75m
                    }
                ]
            },
            new Restaurant
            {
                Name = "Burger Barn",
                Description = "Juicy handcrafted burgers and crispy fries.",
                Category = "American",
                HasDelivery = true,
                ContactEmail = "orders@burgerbarn.com",
                ContactNumber = "+1-202-555-0104",
                Address = new Address("Chicago", "321 Lakeview Dr", "60601"),
                Dishes =
                [
                    new Dish
                    {
                        Name = "Classic Cheeseburger",
                        Description = "Beef patty with cheddar, lettuce, tomato, and special sauce.",
                        Price = 11.50m
                    },
                    new Dish
                    {
                        Name = "Loaded Fries",
                        Description = "Crispy fries topped with cheese, bacon, and green onions.",
                        Price = 6.99m
                    }
                ]
            },
            new Restaurant
            {
                Name = "Curry House",
                Description = "Aromatic Indian curries and freshly baked naan.",
                Category = "Indian",
                HasDelivery = true,
                ContactEmail = "contact@curryhouse.com",
                ContactNumber = "+1-202-555-0105",
                Address = new Address("Seattle", "654 Pine St", "98101"),
                Dishes =
                [
                    new Dish
                    {
                        Name = "Butter Chicken",
                        Description = "Tender chicken in a rich and creamy tomato sauce.",
                        Price = 13.99m
                    },
                    new Dish
                    {
                        Name = "Vegetable Biryani",
                        Description = "Fragrant basmati rice cooked with spiced vegetables.",
                        Price = 10.50m
                    }
                ]
            }
        ];

        return restaurants;
    }
}